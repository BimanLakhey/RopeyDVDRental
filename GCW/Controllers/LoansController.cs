#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GCW.Areas.Identity.Data;
using GCW.Models;

namespace GCW.Controllers
{
    public class LoansController : Controller
    {
        private readonly ApplicationDBContext _context;

        public LoansController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Loans
        public async Task<IActionResult> Index()
        {
            var applicationDBContext = _context.Loan.Include(l => l.DVDCopy).Include(l => l.LoanType).Include(l => l.Member);
            return View(await applicationDBContext.ToListAsync());
        }

        // GET: Loans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loan
                .Include(l => l.DVDCopy)
                .Include(l => l.LoanType)
                .Include(l => l.Member)
                .FirstOrDefaultAsync(m => m.LoanNumber == id);
            if (loan == null)
            {
                return NotFound();
            }

            return View(loan);
        }

        // GET: Loans/Create
        public IActionResult Create()
        {
            ViewData["CopyNumber"] = new SelectList(_context.DVDCopy, "CopyNumber", "CopyNumber");
            ViewData["LoanTypeNumber"] = new SelectList(_context.LoanType, "LoanTypeNumber", "LoanTypeNumber");
            ViewData["MemberNumber"] = new SelectList(_context.Member, "MemberNumber", "MemberNumber");
            return View();
        }

        // POST: Loans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LoanNumber,LoanTypeName,CopyNumber,MemberNumber")] Loan loan)
        {
            var dvdCopies = _context.DVDCopy;
            var members = _context.Member;
            var loanTypes = _context.LoanType;
            var loans = _context.Loan;

            var loanTypeStr = HttpContext.Request.Form["LoanTypeNumber"];
            LoanType loanType = loanTypes.Where(l => l.LoanTypeNumber == int.Parse(loanTypeStr)).FirstOrDefault();

            Member member = members.Where(l => l.MemberNumber == loan.MemberNumber).Include(m => m.MembershipCategory).FirstOrDefault();
            DVDCopy dvdCopy = dvdCopies.Where(l => l.CopyNumber == loan.CopyNumber).Include(c => c.DVDTitle).ThenInclude(d => d.DVDCategory).FirstOrDefault();

            int remainingLoanCount = _context.Loan.Where(l => l.MemberNumber == loan.MemberNumber && l.DateReturned == null).Count();

            int standardCharge = (
                      from dC in _context.DVDCopy
                      join dT in _context.DVDTitle on dC.DVDNumber equals dT.DVDNumber
                      where dC.CopyNumber == loan.CopyNumber
                      select dT.StandardCharge).Single();

            //int loanDuration = (from dC in _context.DVDCopy
            //                   join dT in _context.DVDTitle on dC.DVDNumber equals dT.DVDNumber
            //                   join l in loans on dC.CopyNumber equals l.CopyNumber
            //                   join lT in loanTypes on l.LoanTypeNumber equals lT.LoanTypeNumber
            //                   where dC.CopyNumber == loan.CopyNumber
            //                   select lT.LoanDuration).Single();

            ViewData["CopyNumber"] = new SelectList(_context.DVDCopy, "CopyNumber", "CopyNumber", loan.CopyNumber);
            ViewData["LoanTypeNumber"] = new SelectList(_context.LoanType, "LoanTypeNumber", "LoanTypeNumber", loan.LoanType);
            ViewData["MemberNumber"] = new SelectList(_context.Member, "MemberNumber", "MemberNumber", loan.MemberNumber);
            ViewData["StandardCharge"] = standardCharge;
            //ViewData["LoanDuration"] = loanDuration;


            loan.DateOut = DateTime.Now;

            loan.DateDue = DateTime.Now.AddDays(loanType.LoanDuration);

            if (remainingLoanCount >= member.MembershipCategory.MembershipCategoryTotalLoans)
            {
                ModelState.AddModelError(string.Empty, "The member has already loaned more than their available loans");
                return View();
            }

            if (DateTime.Today.Year - member.MemberDateOfBirth.Year < 18 && bool.Parse(dvdCopy.DVDTitle.DVDCategory.AgeRestriction))
            {
                ModelState.AddModelError(string.Empty, "The DVD is for 18+ members only");
                return View();
            }

            loan.LoanTypeNumber = loanType.LoanTypeNumber;

            if (ModelState.IsValid)
            {
                _context.Add(loan);
                await _context.SaveChangesAsync();
                ModelState.AddModelError(string.Empty, "DVD loaned");
                //return RedirectToAction(nameof(Index));
            }

            return View(loan);
        }

        // GET: Loans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loan.FindAsync(id);
            if (loan == null)
            {
                return NotFound();
            }
            var result = _context.Loan.Where(x => x.LoanNumber == id);
            int penaltyCharge = (from r in result 
                                join dC in _context.DVDCopy on r.CopyNumber equals dC.CopyNumber
                                join dT in _context.DVDTitle on dC.DVDNumber equals dT.DVDNumber
                                select dT.PenaltyCharge).Single();

            ViewData["CopyNumber"] = new SelectList(_context.DVDCopy, "CopyNumber", "CopyNumber", loan.CopyNumber);
            ViewData["LoanTypeNumber"] = new SelectList(_context.LoanType, "LoanTypeNumber", "LoanTypeNumber", loan.LoanTypeNumber);
            ViewData["MemberNumber"] = new SelectList(_context.Member, "MemberNumber", "MemberNumber", loan.MemberNumber);
            ViewData["PenaltyCharge"] = penaltyCharge;
            return View(loan);
        }

        // POST: Loans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LoanNumber,DateOut,DateDue,DateReturned,LoanTypeNumber,CopyNumber,MemberNumber")] Loan loan)
        {
            if (id != loan.LoanNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoanExists(loan.LoanNumber))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CopyNumber"] = new SelectList(_context.DVDCopy, "CopyNumber", "CopyNumber", loan.CopyNumber);
            ViewData["LoanTypeNumber"] = new SelectList(_context.LoanType, "LoanTypeNumber", "LoanTypeNumber", loan.LoanTypeNumber);
            ViewData["MemberNumber"] = new SelectList(_context.Member, "MemberNumber", "MemberNumber", loan.MemberNumber);
            return View(loan);
        }

        // GET: Loans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loan
                .Include(l => l.DVDCopy)
                .Include(l => l.LoanType)
                .Include(l => l.Member)
                .FirstOrDefaultAsync(m => m.LoanNumber == id);
            if (loan == null)
            {
                return NotFound();
            }

            return View(loan);
        }

        // POST: Loans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loan = await _context.Loan.FindAsync(id);
            _context.Loan.Remove(loan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoanExists(int id)
        {
            return _context.Loan.Any(e => e.LoanNumber == id);
        }
    }
}
