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

        // GET: ProjectsPages
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProjectsPages.ToListAsync());
        }

        // GET: ProjectsPages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectsPage = await _context.ProjectsPages
                .FirstOrDefaultAsync(m => m.ProjectsPageId == id);
            if (projectsPage == null)
            {
                return NotFound();
            }

            return View(projectsPage);
        }

        // GET: ProjectsPages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProjectsPages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectsPageId,Text")] ProjectsPage projectsPage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projectsPage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(projectsPage);
        }

        // GET: ProjectsPages/EditTEXT/5
        public async Task<IActionResult> EditText(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectsPage = await _context.ProjectsPages.FindAsync(id);
            if (projectsPage == null)
            {
                return NotFound();
            }
            return View(projectsPage);
        }

        // POST: ProjectsPages/EditTEXT/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditText(int id, [Bind("ProjectsPageId,Text")] ProjectsPage projectsPage)
        {
            if (id != projectsPage.ProjectsPageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projectsPage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectsPageExists(projectsPage.ProjectsPageId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(projectsPage);
        }

        // GET: ProjectsPages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectsPage = await _context.ProjectsPages
                .FirstOrDefaultAsync(m => m.ProjectsPageId == id);
            if (projectsPage == null)
            {
                return NotFound();
            }

            return View(projectsPage);
        }

        // POST: ProjectsPages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projectsPage = await _context.ProjectsPages.FindAsync(id);
            _context.ProjectsPages.Remove(projectsPage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectsPageExists(int id)
        {
            return _context.ProjectsPages.Any(e => e.ProjectsPageId == id);
        }
    }
}
