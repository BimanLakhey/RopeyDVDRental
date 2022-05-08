using GCW.Areas.Identity.Data;
using GCW.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GCW.Controllers
{
    public class ActorDVDCopiesController : Controller
    {

        private readonly ApplicationDBContext _context;

        public ActorDVDCopiesController(ApplicationDBContext context)
        {
            _context = context;
        }



        public async Task<IActionResult> Index(string lastName)
        {
            using (_context)
            {
                List<Actor> actors = _context.Actor.ToList();
                List<DVDCopy> dvdCopies = _context.DVDCopy.ToList();
                List<Loan> loans = _context.Loan.ToList();
                List<DVDTitle> dvdTitles = _context.DVDTitle.ToList();
                List<CastMember> castMembers = _context.CastMember.ToList();

                    var results = (from dt in dvdTitles
                                   join cm in castMembers
                                   on dt.DVDNumber equals cm.DVDNumber
                                   join dc in dvdCopies
                                   .Where(c => _context.Loan.Any(l => (c.CopyNumber == l.CopyNumber && l.DateReturned != null)))
                                   on dt.DVDNumber equals dc.DVDNumber
                                   join a in actors
                                   on cm.ActorNumber equals a.ActorNumber
                                   where a.ActorSurname == lastName
                                   group new { dt, cm, dc } by new { dt.DVDNumber, dt.Title, a.ActorSurname }into grp
                                   select new DVDCopiesOnShelves
                                   {
                                       DVDNumber = grp.Key.DVDNumber,
                                       DVD_Count = grp.Count(),
                                       ActorSurname = grp.Key.ActorSurname,
                                       Title = grp.Key.Title,
                                   }).ToList();

                    ViewData["results"] = results;
                    return View();
            }


        }
        // GET: ActorDVDCopies
        //public Task<IActionResult> Index(string searchString)
        //{
        //    using (_context)
        //    {
        //        List<Actor> actors = _context.Actor.ToList();
        //        List<DVDCopy> dvdCopies = _context.DVDCopy.ToList();
        //        List<Loan> loans = _context.Loan.ToList();
        //        List<DVDTitle> dvdTitles = _context.DVDTitle.ToList();
        //        List<CastMember> castMembers = _context.CastMember.ToList();


        

        //        ViewData["results"] = results;
        //        return View(results);
        //    }

        //}

        // GET: ActorDVDCopies/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ActorDVDCopies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ActorDVDCopies/Create
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

        // GET: ActorDVDCopies/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ActorDVDCopies/Edit/5
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

        // GET: ActorDVDCopies/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ActorDVDCopies/Delete/5
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
