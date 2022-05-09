using GCW.Areas.Identity.Data;
using GCW.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GCW.Controllers
{
    public class MemberLoansController : Controller
    {
        private readonly ApplicationDBContext _context;

        public MemberLoansController(ApplicationDBContext context)
        {
            _context = context;
        }
        // GET: HomeController1

        public async Task<IActionResult> Index()
        {
            var dvdCopies = _context.DVDCopy.ToList();
            var dvdTitles = _context.DVDTitle.ToList();
            var castMembers = _context.CastMember.ToList();
            var actors = _context.Actor.ToList();
            var members = _context.Member.ToList();
            var loans = _context.Loan.ToList();
            var membershipCategories = _context.MembershipCategory.ToList();



            var memberLoanStatus = (from m in members
                                    join l in loans
                                    on m.MemberNumber equals l.MemberNumber
                                    join mC in membershipCategories
                                    on m.MembershipCategoryNumber equals mC.MembershipCategoryNumber
                                    select new MemberLoansViewModel
                                    {
                                        MemberNumber = m.MemberNumber,
                                        MemberFirstName = m.MemberFirstName,
                                        MemberLastName = m.MemberLastName,
                                        MemberAddress = m.MemberAddress,
                                        DateReturned = l.DateReturned,
                                        MemberDateOfBirth = m.MemberDateOfBirth,
                                        MembershipCategoryDesc = mC.MembershipCategoryDescription.ToString(),
                                        CategoryTotalLoans = mC.MembershipCategoryTotalLoans

                                    }).GroupBy(x => x.MemberNumber).Select(g => new MemberLoansViewModel
                                    {
                                        MemberNumber = g.Key,
                                        MemberFirstName = g.FirstOrDefault().MemberFirstName,
                                        MemberLastName = g.FirstOrDefault().MemberLastName,
                                        MemberAddress = g.FirstOrDefault().MemberAddress,
                                        MemberDateOfBirth = g.FirstOrDefault().MemberDateOfBirth,
                                        MembershipCategoryDesc = g.FirstOrDefault().MembershipCategoryDesc,
                                        CategoryTotalLoans = g.FirstOrDefault().CategoryTotalLoans,
                                        LoanCount = g.Count(l => l.DateReturned == null),
                                        LoanStatus = (g.FirstOrDefault().CategoryTotalLoans < g.Count(l => l.DateReturned == null)) ? "Too Many DVDs" : ""
                                    });
            return View(memberLoanStatus);
        }

        // GET: HomeController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController1/Create
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

        // GET: HomeController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController1/Edit/5
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

        // GET: HomeController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController1/Delete/5
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
