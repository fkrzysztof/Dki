using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sasso.Data.Data;
using Sasso.Data.Data.Data;
using Sasso.Edit.Controllers.Abstract;

namespace Sasso.Edit.Controllers
{
    public class AboutController : AbstractController
    {
        public AboutController(ILogger<HomeController> logger, WebContext context)
        : base(logger, context)
        {
        }


        // GET: About/Edit/5
        public async Task<IActionResult> Index()
        {
            var about = await _context.Abouts.FirstOrDefaultAsync();
            if (about == null)
            {
                _context.Abouts.Add(new About() { Maintext = "", Text = "" });
                _context.SaveChanges();
            }

            return View("Edit", await _context.Abouts.FirstAsync());
        }

        // POST: About/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("AboutID,Maintext,Text")] About about)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(about);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AboutExists(about.AboutID))
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
            return View(about);
        }


        private bool AboutExists(int id)
        {
            return _context.Abouts.Any(e => e.AboutID == id);
        }

        public async Task<IActionResult> AddPartners(IFormFile[] partnersImg)
        {
            int idAbout = _context.Abouts.First().AboutID;
            if (partnersImg != null)
            {
                foreach (var item in partnersImg)
                {
                    //_context.Partners.Add(new Partners() { MediaItem = new CloudAccess().AddPic(item, "Partners"), AboutID = idAbout });
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult DeletePartnersImg(int id)
        {
            if (DeletePartnersImgJS(id))
                return RedirectToAction("Index");
            else
                return NotFound();
        }
        public bool DeletePartnersImgJS(int id)
        {
            bool output;
            var item = _context.Partners.FirstOrDefault(f => f.PartnersID == id);
            if(item != null)
            {
                var rezult = _context.Partners.Remove(item);
                output = rezult.State.ToString() == "Deleted";
                _context.SaveChanges();
                return output;
            }
            return false;

        }


    }
}
