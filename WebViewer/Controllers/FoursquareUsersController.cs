using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using WebViewer.Models;

namespace WebViewer.Controllers
{
    public class FoursquareUsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FoursquareUsersController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: FoursquareUser
        public async Task<IActionResult> Index()
        {
            return View(await _context.FoursquareUsers.ToListAsync());
        }

        // GET: FoursquareUser/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            FoursquareUser foursquareUser = await _context.FoursquareUsers.SingleAsync(m => m.Id == id);
            if (foursquareUser == null)
            {
                return HttpNotFound();
            }

            return View(foursquareUser);
        }

        // GET: FoursquareUser/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        [Route("oauth2")]
        public IActionResult FoursquareOauth([FromQuery] string uri)
        {
            if (ModelState.IsValid)
            {
                var factory = FourSquareFactory.Create();
                factory.Authenticate(uri);
                var foursquareUser = new FoursquareUser {Id = Guid.NewGuid()};
                _context.FoursquareUsers.Add(foursquareUser);
                _context.SaveChangesAsync();
            }
            
            return RedirectToAction("Index");
        }

        // POST: FoursquareUser/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FoursquareUser foursquareUser)
        {
            if (ModelState.IsValid)
            {
                var factory = FourSquareFactory.Create();
                var authURI = factory.GetAuthenticationURI();
                return Redirect(authURI);
            }
            return View(foursquareUser);
        }

        // GET: FoursquareUser/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            FoursquareUser foursquareUser = await _context.FoursquareUsers.SingleAsync(m => m.Id == id);
            if (foursquareUser == null)
            {
                return HttpNotFound();
            }
            return View(foursquareUser);
        }

        // POST: FoursquareUser/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(FoursquareUser foursquareUser)
        {
            if (ModelState.IsValid)
            {
                _context.Update(foursquareUser);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(foursquareUser);
        }

        // GET: FoursquareUser/Delete/5
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            FoursquareUser foursquareUser = await _context.FoursquareUsers.SingleAsync(m => m.Id == id);
            if (foursquareUser == null)
            {
                return HttpNotFound();
            }

            return View(foursquareUser);
        }

        // POST: FoursquareUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            FoursquareUser foursquareUser = await _context.FoursquareUsers.SingleAsync(m => m.Id == id);
            _context.FoursquareUsers.Remove(foursquareUser);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
