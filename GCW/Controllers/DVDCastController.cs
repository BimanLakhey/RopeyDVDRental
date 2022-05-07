using GCW.Areas.Identity.Data;
using GCW.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GCW.Controllers
{
    public class DVDCastController : Controller
    {
        // GET: DVDCast
        private readonly ApplicationDBContext _context;

        public DVDCastController(ApplicationDBContext context)
        {
            _context = context;
        }
        public ActionResult Index()
        {
            using (_context)
            {
                List<DVDTitle> dvdTitles = _context.DVDTitle.ToList();
                List<Producer> producers = _context.Producer.ToList();
                List<Studio> studios = _context.Studio.ToList();
                List<CastMember> castMembers = _context.CastMember.ToList();
                List<Actor> actors = _context.Actor.ToList();


                var dvdCastRecord = from dT in dvdTitles
                                   join p in producers on dT.ProducerNumber equals p.ProducerNumber
                                   join s in studios on dT.StudioNumber equals s.StudioNumber
                                   join c in castMembers on dT.DVDNumber equals c.DVDNumber
                                   join a in actors on c.ActorNumber equals a.ActorNumber
                                   orderby dT.DateReleased ascending
                                   orderby a.ActorSurname ascending
                                   select new DVDCastViewModel
                                   {
                                       dvdTitle = dT,
                                       producer = p,
                                       studio = s,
                                       castMember = c,
                                       actor = a,
                                   };

                return View(dvdCastRecord);
            }
        }

        // GET: DVDCast/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DVDCast/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DVDCast/Create
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

        // GET: DVDCast/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DVDCast/Edit/5
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

        // GET: DVDCast/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DVDCast/Delete/5
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
