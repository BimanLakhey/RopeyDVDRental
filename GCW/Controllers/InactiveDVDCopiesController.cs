using GCW.Areas.Identity.Data;
using GCW.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GCW.Controllers
{
    public class InactiveDVDCopiesController : Controller
    {
        private readonly ApplicationDBContext _context;

        public InactiveDVDCopiesController(ApplicationDBContext context)
        {
            _context = context;
        }
        // GET: InactiveDVDCopiesController
        public ActionResult Index()
        {
            using (_context)
            {
                List<DVDTitle> dvdTitles = _context.DVDTitle.ToList();
                List<DVDCopy> dvdCopies = _context.DVDCopy.ToList();
                List<Loan> loans = _context.Loan.ToList();

                var activeDVDsList = (from dT in dvdTitles
                                         join dC in dvdCopies on dT.DVDNumber equals dC.DVDNumber into table1
                                         from dC in table1
                                         join l in loans on dC.CopyNumber equals l.CopyNumber into table2
                                         from l in table2
                                         where l.DateOut >= DateTime.Now.AddDays(-31)
                                         select dT);

                var inactiveDVDsList = dvdTitles.Except(activeDVDsList);

                return View(inactiveDVDsList);
            }
        }

        // GET: InactiveDVDCopiesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: InactiveDVDCopiesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InactiveDVDCopiesController/Create
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

        // GET: InactiveDVDCopiesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: InactiveDVDCopiesController/Edit/5
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

        // GET: InactiveDVDCopiesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: InactiveDVDCopiesController/Delete/5
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
