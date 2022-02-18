using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sald.Edit.Controllers.Abstract;
using Sasso.Data.Data;
using Sasso.Edit.Models;

namespace Sasso.Edit.Controllers
{
    public class HomeController : BaseAbstractController
    {
        public HomeController(WebContext context)
        : base(context)
        {
        }

        // GET: Page WWW
        public async Task<IActionResult> Page(int id)
        {
            await sendAsync();
            return View(await _context.Offers.Include(i => i.Image).FirstOrDefaultAsync(f => f.OfferID == id));
        }

        public async Task<IActionResult> Index()
        {
            //await sendAsync();
            //include image ?
            ViewBag.Offer = await _context.Offers.Include(i => i.Image).ToListAsync();
            var projects = await _context.Projects.Where(w => w.Active == true && DateTime.Compare(w.StartProject, DateTime.Now) <= 0 &&
                                            DateTime.Compare(w.EndProject, DateTime.Now) >= 0).ToListAsync();
            if(projects.Count() == 0)
                projects = await _context.Projects.Where(w => w.Active == true && DateTime.Compare(w.EndProject, DateTime.Now) < 0).ToListAsync();

            ViewBag.Projects = projects;

            //ViewBag.About = await _context.Abouts.FirstAsync();
            return View();

            //return RedirectToAction("Index","About");
            //return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region JS
        
        public string LogoJS()
        {
            var settings = _context.Settings.Include(i => i.Logo).FirstOrDefault();
            if (settings != null && settings.Logo != null)
                return settings.Logo.Path;
            else
                return "";
        }
        #endregion


        #region share
        public IActionResult Fb()
        {
            return Redirect("http://www.facebook.com/sharer/sharer.php?u=http://www.sald.com.pl");
        }
        public IActionResult Twitter()
        {
            return Redirect("https://twitter.com/intent/tweet?text=http://www.sald.com.pl");
        }
        public IActionResult Whatsapp()
        {
            return Redirect("https://api.whatsapp.com/send?text=%0ahttp://www.sald.com.pl");
        }
        #endregion share
    }
}
