using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sald.Data.Data.Data;
using Sald.Data.HelperClass;
using Sasso.Data.Data;
using Sasso.Edit.Controllers;
using Sasso.Edit.Controllers.Abstract;

namespace Sald.Edit.Controllers
{
    public class OfferController : AbstractController
    {
        public OfferController(ILogger<HomeController> logger, WebContext context)
        : base(logger, context)
        {
        }


        // GET: Offers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Offers.Include(i => i.Image).ToListAsync());
        }


        // GET: Offers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Offers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OfferID,Name,Text,TextMain,FormFileItem,LinkName")] Offer offer)
        {
            if (ModelState.IsValid)
            {
                if (offer.FormFileItem != null)
                {
                    offer.Image = FileAction.UploadFiles(offer.FormFileItem).Result.First();
                }

                _context.Add(offer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(offer);
        }

        // GET: Offers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offer = await _context.Offers.Include(i => i.Image).FirstOrDefaultAsync(f => f.OfferID == id);
            
            string imgPath;
            if (offer.Image == null)
                imgPath = null;
            else 
                imgPath = offer.Image.Path;
            ViewBag.OfferImg = imgPath;

            return View(offer);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OfferID,Name,Text,TextMain,FormFileItem,LinkName")] Offer offer, string path)
        {
            if (id != offer.OfferID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    if (offer.FormFileItem != null && !string.IsNullOrEmpty(path))
                    {
                        var img = await _context.MyFiles.FirstOrDefaultAsync(f => f.OfferID == id);
                        img.Path = FileAction.ChangeFile(path, offer.FormFileItem).Path;
                        _context.Update(img);
                        _context.SaveChanges();
                    }
                    else if (offer.FormFileItem != null)
                    {
                        offer.Image = FileAction.UploadFiles(offer.FormFileItem).Result.First();
                    }

                    _context.Update(offer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfferExists(offer.OfferID))
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
            return View(offer);
        }


        [HttpPost]
        public async Task<bool> DeleteJS(int id)
        {
            if (id < 0)
                return false;
            bool output;
            var offer = await _context.Offers.Include(i => i.Image).FirstOrDefaultAsync(f => f.OfferID == id);
            if (offer == null)
                return false;
            
            FileAction.RemoveFile(offer.Image);
            var rezultFile = _context.MyFiles.Remove(offer.Image);
            var rezultOffer = _context.Offers.Remove(offer);

            if (rezultFile.State.ToString() == "Deleted" && rezultOffer.State.ToString() == "Deleted")
                output = true;
            else
                output = false;
            await _context.SaveChangesAsync();

            return output;
        }


        // GET: Offers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offer = await _context.Offers
                .FirstOrDefaultAsync(m => m.OfferID == id);
            if (offer == null)
            {
                return NotFound();
            }

            return View(offer);
        }

        // POST: Offers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            if (await DeleteJS(id))
                return RedirectToAction(nameof(Index));
            else
                return NotFound();
        }

        private bool OfferExists(int id)
        {
            return _context.Offers.Any(e => e.OfferID == id);
        }
    }


}