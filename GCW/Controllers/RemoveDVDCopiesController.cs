using GCW.Areas.Identity.Data;
using GCW.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GCW.Controllers
{
    public class RemoveDVDCopiesController : Controller
    {
        // GET: RemoveDVDCopiesController
        private readonly ApplicationDBContext _context;

        public RemoveDVDCopiesController(ApplicationDBContext context)
        {
            _context = context;
        }
        public ActionResult Index()
        {
            using (_context)
            {
                List<DVDTitle> dvdTitles = _context.DVDTitle.ToList();
                List<DVDCopy> dvdCopies = _context.DVDCopy.ToList();
                List<Loan> loans = _context.Loan.ToList();

                var removeDVDsList = from dT in dvdTitles
                                    join dC in dvdCopies on dT.DVDNumber equals dC.DVDNumber into table1
                                    from dC in table1
                                    join l in loans on dC.CopyNumber equals l.CopyNumber into table2
                                    from l in table2
                                    where dC.DatePurchased < DateTime.Now.AddDays(-365)
                                    where l.DateReturned is not null
                                    select new RemoveDVDCopiesViewModel
                                    {
                                        dvdTitle = dT,
                                        dvdCopy = dC,
                                        loan = l,
                                    };

                return View(removeDVDsList);
            }
        }

        // GET: RemoveDVDCopiesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RemoveDVDCopiesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RemoveDVDCopiesController/Create
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

        // GET: RemoveDVDCopiesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RemoveDVDCopiesController/Edit/5
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

        // GET: RemoveDVDCopiesController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dvdCopy = await _context.DVDCopy
                .Include(d => d.DVDTitle)
                .FirstOrDefaultAsync(m => m.CopyNumber == id);
            if (dvdCopy == null)
            {
                return NotFound();
            }

            return View(dvdCopy);
        }

        // POST: RemoveDVDCopiesController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dvdCopy = await _context.DVDCopy.FindAsync(id);
            _context.DVDCopy.Remove(dvdCopy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool DVDCopyExists(int id)
        {
            return _context.DVDCopy.Any(e => e.CopyNumber == id);
        }
    }
}
