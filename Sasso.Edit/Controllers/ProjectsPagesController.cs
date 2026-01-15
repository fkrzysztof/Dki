using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sasso.Data.Data;
using Sasso.Data.Data.Data;
using Sasso.Edit.Controllers.Abstract;

namespace Sasso.Edit.Controllers
{
    public class ProjectsPagesController : AbstractController
    {

        public ProjectsPagesController(ILogger<HomeController> logger, WebContext context)
        : base(logger, context)
        {
        }


        private void SendHowManyItems()
        {

            ViewBag.Index = _context.Projects.Where(w => w.Active == true && DateTime.Compare(w.StartProject, DateTime.Now) <= 0 &&
                                                        DateTime.Compare(w.EndProject, DateTime.Now) >= 0).Count();
            ViewBag.Ended = _context.Projects.Where(w => w.Active == true && DateTime.Compare(w.EndProject, DateTime.Now) < 0).Count();
            ViewBag.Deleted = _context.Projects.Where(w => w.Active == false).Count();
            ViewBag.All = _context.Projects.Count();
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            SendHowManyItems();

            var projectPage = await _context.ProjectsPages.FirstOrDefaultAsync();
            if (projectPage == null)
            {
                _context.ProjectsPages.Add(new ProjectsPage() { Text = "" });
                _context.SaveChanges();
            }

            var projectsList = await _context.Projects.Include(i => i.Image).Where(w => w.Active == true && DateTime.Compare(w.StartProject, DateTime.Now) <= 0 &&
                                                        DateTime.Compare(w.EndProject, DateTime.Now) >= 0).ToListAsync();

            if (projectsList.Count == 0)
            {
                return RedirectToAction("All");
            }
            else
            {
                ViewBag.Project = projectsList;
                return View(await _context.ProjectsPages.FirstAsync());
            }

        }

        public async Task<IActionResult> Ended()
        {
            SendHowManyItems();
            ViewBag.Project = await _context.Projects.Include(i => i.Image).Where(w => w.Active == true && DateTime.Compare(w.EndProject, DateTime.Now) < 0).ToListAsync();
            return View("Index", await _context.ProjectsPages.FirstAsync());
        }

        public async Task<IActionResult> Deleted()
        {
            ViewBag.Project = await _context.Projects.Include(i => i.Image).Where(w => w.Active == false).ToListAsync();
            SendHowManyItems();
            return View("Index", await _context.ProjectsPages.FirstAsync());
        }

        public async Task<IActionResult> All()
        {
            SendHowManyItems();
            ViewBag.Project = await _context.Projects.Include(i => i.Image).ToListAsync();
            return View("Index", await _context.ProjectsPages.FirstAsync());
        }


    }
}
