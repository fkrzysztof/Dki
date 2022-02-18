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
            //var offer = await _context.Offers.Include(i => i.Image).ToListAsync();
            //ViewBag.About = await _context.Abouts.FirstAsync();
            //return View(offer);

            return RedirectToAction("Index","About");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region JS
        [HttpPost]
        public string LogoJS()
        {
            return _context.Settings.Include(i => i.Logo).FirstOrDefault().Logo.Path;
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
