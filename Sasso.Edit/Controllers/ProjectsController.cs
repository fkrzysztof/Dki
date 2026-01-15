using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sald.Data.HelperClass;
using Sasso.Data.Data;
using Sasso.Data.Data.Data;
using Sasso.Edit.Controllers.Abstract;


namespace Sasso.Edit.Controllers
{
    public class ProjectsController : AbstractController
    {

        public ProjectsController(ILogger<HomeController> logger, WebContext context)
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

        #region Index Pages

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
        #endregion

        //do poprawki !!!!!!!!!!!!!!!!
        #region Preview


        public IActionResult About(int id)
        {
            //ViewBag.Project = _context.Projects.FirstOrDefault(f => f.ProjectsID == id).About;
            //ViewBag.Name = _context.Projects.FirstOrDefault(f => f.ProjectsID == id).Name;
            ViewBag.ProjectId = id;
            return View("Preview");
        }

        public IActionResult News(int id)
        {
            ViewBag.Project = _context.Projects.FirstOrDefault(f => f.ProjectsID == id).News;
            ViewBag.Name = _context.Projects.FirstOrDefault(f => f.ProjectsID == id).Name;
            ViewBag.ProjectId = id;
            return View("Preview");
        }

        public IActionResult Participants(int id)
        {
            ViewBag.Project = _context.Projects.FirstOrDefault(f => f.ProjectsID == id).Participants;
            ViewBag.Name = _context.Projects.FirstOrDefault(f => f.ProjectsID == id).Name;
            ViewBag.ProjectId = id;
            return View("Preview");
        }

        public IActionResult FormOfSupport(int id)
        {
            ViewBag.Project = _context.Projects.FirstOrDefault(f => f.ProjectsID == id).FormOfSupport;
            ViewBag.Name = _context.Projects.FirstOrDefault(f => f.ProjectsID == id).Name;
            ViewBag.ProjectId = id;
            return View("Preview");
        }

        public IActionResult Recruitment(int id)
        {
            ViewBag.Project = _context.Projects.FirstOrDefault(f => f.ProjectsID == id).Recruitment;
            ViewBag.Name = _context.Projects.FirstOrDefault(f => f.ProjectsID == id).Name;
            ViewBag.ProjectId = id;
            return View("Preview");
        }

        public IActionResult Contact(int id)
        {
            ViewBag.Project = _context.Projects.FirstOrDefault(f => f.ProjectsID == id).Contact;
            ViewBag.Name = _context.Projects.FirstOrDefault(f => f.ProjectsID == id).Name;
            ViewBag.ProjectId = id;
            return View("Preview");
        }

        #endregion


        // GET: Projects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectsID,Name,News,About,Participants,FormOfSupport,Recruitment,Contact," +
            "DateOfPublication,MediaItem,FormFileItem,StartProject,EndProject")] Projects projects, IFormFile[] Files)
        {
            if (ModelState.IsValid)
            {
                if (projects.FormFileItem != null)
                {
                    //projects.Image = new CloudAccess().AddPic(projects.FormFileItem, "Projects");
                    projects.Image = FileAction.UploadFiles(projects.FormFileItem).Result.FirstOrDefault();
                }

                if (Files.Length > 0)
                {
                    //projects.Files = await UploadFiles(Files);
                    projects.Files = await FileAction.UploadFiles(Files);
                }

            projects.Active = true;
                _context.Add(projects);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(projects);
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projects = await _context.Projects.Include(i => i.Files).Include(i => i.Image).FirstAsync(f => f.ProjectsID == id);
            if (projects == null)
            {
                return NotFound();
            }
            return View(projects);
        }

        //POST: Projects/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectsID,Name,News,About,Participants,FormOfSupport,Recruitment,Contact," +
            "DateOfPublication,MediaItem,FormFileItem,StartProject,EndProject,Active")] Projects projects, IFormFile[] Files)
        {
            if (id != projects.ProjectsID)
            {
                return NotFound();
            }

            projects.Image = _context.MyFiles.FirstOrDefault(f => f.ProjectsID == projects.ProjectsID);

            if (ModelState.IsValid)
            {
                try
                {
                    if (projects.FormFileItem != null && projects.Image != null)
                    {
                        //projects.MediaItem = new CloudAccess().ChangeItem(projects.MediaItem, projects.FormFileItem, "Projects");
                        projects.Image = FileAction.ChangeFile(projects.Image.Path, projects.FormFileItem);
                    }
                    else if (projects.FormFileItem != null && projects.Image == null)
                    {
                        //projects.MediaItem = new CloudAccess().AddPic(projects.FormFileItem, "Projects");
                        projects.Image = FileAction.UploadFiles(projects.FormFileItem).Result.FirstOrDefault();
                    }
                    //_context.Update(projects);
                    //await _context.SaveChangesAsync();
                    if (Files.Length > 0)
                    {
                        //projects.Files = await UploadFiles(Files);
                        projects.Files = await FileAction.UploadFiles(Files);
                    }
                    _context.Update(projects);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectsExists(projects.ProjectsID))
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
            return View(projects);
        }


        #region in case JS does not work
        
        // GET: Projects/Delete/5 - in case JS does not work
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projects = await _context.Projects
                .FirstOrDefaultAsync(m => m.ProjectsID == id);

            if (projects == null)
            {
                return NotFound();
            }

            ViewBag.Message = "Czy na pewno chcesz to usunąć?";

            return View(projects);
        }

        //POST: Projects/Delete/5 -in case JS does not work
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (ActiveStatusJS(id).Result == true)
                return RedirectToAction(nameof(Index));
            else
                return NotFound();
        }

        public IActionResult Kill(int id)
        {
            if (KillJS(id))
                return RedirectToAction("Index");
            else
                return NotFound();
        }

        #endregion

        //JS DELETE - STATUS ACTIVE
        [HttpPost]
        public async Task<bool> ActiveStatusJS(int id)
        {
            bool output;
            var projects = await _context.Projects.FirstOrDefaultAsync(f => f.ProjectsID == id);
            if (projects == null || id < 0)
                return false;

            projects.Active = !projects.Active;

            try
            {
                var rezult = _context.Projects.Update(projects);
                if (rezult.State.ToString() == "Modified")
                    output = true;
                else
                    output = false;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectsExists(projects.ProjectsID))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return output;
        }


        // GET: Projects/Restore - in case JS does not work
        public IActionResult Restore(int id)
        {
            if (ActiveStatusJS(id).Result == true)
                return RedirectToAction(nameof(Index));
            else
                return NotFound();
        }


        [HttpPost]
        public bool KillJS(int id)
        {
            var killIt = _context.Projects.Include(i => i.Image).Include(i => i.Files).FirstOrDefault(f => f.ProjectsID == id);
            bool output;
            if(killIt != null )
            {
                if(killIt.Image != null)
                {
                    //new CloudAccess().Remove(killIt.MediaItem);
                    FileAction.RemoveFile(killIt.Image);
                    _context.RemoveRange(_context.MyFiles.Where(w => w.ProjectsID == id));
                }

                if (killIt.Files != null)
                {
                    FileAction.RemoveFile(killIt.Files);
                }

                var rezult  = _context.Projects.Remove(killIt);
                output = rezult.State.ToString() == "Deleted";
                _context.SaveChanges();
                return output;
            }
            return false;
        }

        #region Edit Text
        
        
        // POST: ProjectsPages/EditTEXT/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditText([Bind("ProjectsPageId,Text")] ProjectsPage projectsPage)
        {
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
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectsPageExists(int id)
        {
            return _context.ProjectsPages.Any(e => e.ProjectsPageId == id);
        }

        #endregion

        private bool ProjectsExists(int id)
        {
            return _context.Projects.Any(e => e.ProjectsID == id);
        }


        [HttpPost]
        public bool RemoveFileJS(int id)
        {
            var killIt = _context.MyFiles.FirstOrDefault(f => f.FileID == id);
            bool output;
            if (killIt != null)
            {
                FileAction.RemoveFile(killIt);
                var rezult = _context.MyFiles.Remove(killIt);
                output = rezult.State.ToString() == "Deleted";
                _context.SaveChanges();

                return output;
            }
            return false;
        }

        //delete file to download - no image
        //public async Task<bool> DeleteFileJS(int id)
        //{
        //    bool output;
        //    var file = await _context.MyFiles.FirstOrDefaultAsync(f => f.FileID == id);
        //    if (file == null || id < 0)
        //        return false;

        //    if (System.IO.File.Exists(MyServer.MapPath("UploadFile\\") + file.Path))
        //    {
        //        System.IO.File.Delete(MyServer.MapPath("UploadFile\\") + file.Path);
        //    }

        //    try
        //    {
        //        var rezult = _context.Files.Remove(file);
        //        if (rezult.State.ToString() == "Deleted")
        //            output = true;
        //        else
        //            output = false;

        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ProjectsExists(file.FileID))
        //        {
        //            return false;
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return output;
        //}


        [AllowAnonymous]
        public async Task<ActionResult> DownloadFileJS(int id)
        {

            var file = _context.MyFiles.FirstOrDefault(f => f.FileID == id);

            if (file == null)
                return NotFound();

            return await FileAction.DownloadFile(file);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProjects(string page)
        {
            SendHowManyItems();

            if(page == "All")
                  return View("UpdateProjects", await _context.Projects.Include(i => i.Image).ToListAsync());
            else if (page == "Ended")
                  return View("UpdateProjects", await _context.Projects.Include(i => i.Image).Where(w => w.Active == true && DateTime.Compare(w.EndProject, DateTime.Now) < 0).ToListAsync());
            else if (page == "Deleted")
                  return View("UpdateProjects", await _context.Projects.Include(i => i.Image).Where(w => w.Active == false).ToListAsync());
            else if (page == "Index")
                  return View("UpdateProjects", await _context.Projects.Include(i => i.Image).Where(w => w.Active == true && DateTime.Compare(w.StartProject, DateTime.Now) <= 0 &&
                                                        DateTime.Compare(w.EndProject, DateTime.Now) >= 0).ToListAsync());
            else
            return NotFound();
        }
    }
}

