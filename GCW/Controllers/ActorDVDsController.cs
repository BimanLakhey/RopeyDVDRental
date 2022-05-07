using GCW.Areas.Identity.Data;
using GCW.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GCW.Controllers
{
    public class ActorDVDsController : Controller
    {
        private readonly ApplicationDBContext _context;

        public ActorDVDsController(ApplicationDBContext context)
        {
            _context = context;
        }
        // GET: ActorDVDsController
        public async Task<IActionResult> Index(string lastName)
        {
            using (_context)
            {
                List<Actor> actors = _context.Actor.ToList();
                List<CastMember> castMembers = _context.CastMember.ToList();
                List<DVDTitle> dvdTitles = _context.DVDTitle.ToList();

                if(!String.IsNullOrEmpty(lastName))
                {
                    var actorDVDs = (from a in actors
                                     join c in castMembers on a.ActorNumber equals c.ActorNumber into table1
                                     from c in table1
                                     join dT in dvdTitles on c.DVDNumber equals dT.DVDNumber
                                     where a.ActorSurname == lastName
                                     select dT).ToList();


                    return View(actorDVDs);
                }

                return View(await _context.DVDTitle.ToListAsync());
            }


        }

        // GET: ActorDVDsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ActorDVDsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ActorDVDsController/Create
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

        // GET: ActorDVDsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ActorDVDsController/Edit/5
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

        // GET: ActorDVDsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ActorDVDsController/Delete/5
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
