using System;
using System.Collections.Generic;
using System.Linq;
using GCW.Areas.Identity.Data;
using GCW.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GCW.Controllers
{
    public class LoanedDVDsController : Controller
    {
        // GET: LoanedDVDs

        private readonly ApplicationDBContext _context;

        public LoanedDVDsController(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string lastName)
        {
            using (_context)
            {
                List<Member> members = _context.Member.ToList();
                List<DVDCopy> dvdCopies = _context.DVDCopy.ToList();
                List<Loan> loans = _context.Loan.ToList();
                List<DVDTitle> dvdTitles = _context.DVDTitle.ToList();


                    var memberRecord = (from m in members
                                       join l in loans on m.MemberNumber equals l.MemberNumber into table1
                                       from l in table1.ToList()
                                       join dC in dvdCopies on l.CopyNumber equals dC.CopyNumber into table2
                                       from dC in table2.ToList()
                                       join dT in dvdTitles on dC.DVDNumber equals dT.DVDNumber into table3
                                       from dT in table3.ToList()
                                       where (l.DateOut >= DateTime.Now.AddDays(-31)) && m.MemberLastName == lastName
                                       select new ViewModel
                                       {
                                           member = m,
                                           loan = l,
                                           dvdCopy = dC,
                                           dvdTitle = dT
                                       }).ToList();

                    return View(memberRecord);
               
                
            }
        }

        // GET: LoanedDVDs/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LoanedDVDs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoanedDVDs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoanedDVDs/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LoanedDVDs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoanedDVDs/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LoanedDVDs/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
