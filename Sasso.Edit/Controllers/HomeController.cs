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

        public async Task<IActionResult> Index()
        {
            ViewBag.AboutText = _context.Abouts.First().Text;
            ViewBag.AboutTextMain = _context.Abouts.First().Maintext;
            await sendAsync();
            return View();
        }

        public async Task<IActionResult> Page(int id)
        {
            await sendAsync();
            ViewBag.OfferId = id;
            return View();        
        }


        #region Projects
        public async Task<IActionResult> Projects()
        {
            await sendAsync();
            SendHowManyItems();

            if (ViewBag.Index == 0)
                return RedirectToAction("All");
            else
                return View("Projects", await _context.Projects.Include(i => i.Image).Where(w => w.Active == true && DateTime.Compare(w.EndProject, DateTime.Now) >= 0).ToListAsync());

        }

        public async Task<IActionResult> Ended()
        {
            await sendAsync();
            SendHowManyItems();
            return View("Projects", await _context.Projects.Include(i => i.Image).Where(w => w.Active == true && DateTime.Compare(w.EndProject, DateTime.Now) < 0).ToListAsync());
        }

        public async Task<IActionResult> All()
        {
            await sendAsync();
            SendHowManyItems();
            return View("Projects", await _context.Projects.Include(i => i.Image).Where(w => w.Active == true).ToListAsync());
        }

        #endregion

        #region Preview

        public async Task<IActionResult> About(int id)
        {
            await sendAsync();
            return View("Preview", _context.Projects.Include(i => i.Image).FirstOrDefault(f => f.ProjectsID == id));
        }

        public async Task<IActionResult> News(int id)
        {
            await sendAsync();
            return View("Preview", _context.Projects.Include(i => i.Image).FirstOrDefault(f => f.ProjectsID == id));
        }

        public async Task<IActionResult> Participants(int id)
        {
            await sendAsync();
            return View("Preview", _context.Projects.Include(i => i.Image).FirstOrDefault(f => f.ProjectsID == id));
        }

        public async Task<IActionResult> FormOfSupport(int id)
        {
            await sendAsync();
            return View("Preview", _context.Projects.Include(i => i.Image).FirstOrDefault(f => f.ProjectsID == id));
        }

        public async Task<IActionResult> Recruitment(int id)
        {
            await sendAsync();
            return View("Preview", _context.Projects.Include(i => i.Image).FirstOrDefault(f => f.ProjectsID == id));
        }

        public async Task<IActionResult> Contact(int id)
        {
            await sendAsync();
            return View("Preview", _context.Projects.Include(i => i.Image).FirstOrDefault(f => f.ProjectsID == id));
        }

        #endregion

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

        private void SendHowManyItems()
        {

            ViewBag.Index = _context.Projects.Where(w => w.Active == true && DateTime.Compare(w.StartProject, DateTime.Now) <= 0 &&
                                                        DateTime.Compare(w.EndProject, DateTime.Now) >= 0).Count();
            ViewBag.Ended = _context.Projects.Where(w => w.Active == true && DateTime.Compare(w.EndProject, DateTime.Now) < 0).Count();
            ViewBag.Deleted = _context.Projects.Where(w => w.Active == false).Count();
            ViewBag.All = _context.Projects.Count();
            ViewBag.ProjectPageText = _context.ProjectsPages.FirstOrDefault().Text;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
