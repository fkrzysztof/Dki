using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sasso.Data.Data;

namespace Sald.Edit.Controllers.Abstract
{
    public class BaseAbstractController : Controller
    {
        protected readonly WebContext _context;

        public BaseAbstractController(WebContext context)
        {
            _context = context;
        }

        public async Task sendAsync()
        {
            Random rand = new Random();
            var backgroundList = _context.MyFiles.Where(w => w.BackgroundListID != null);
            int toSkip = rand.Next(0, backgroundList.Count());
            var bg = backgroundList.Skip(toSkip).First().Path;
            bg = bg.Replace("\\", "/");
            ViewBag.Bg = bg;
            var contact = _context.Contacts.FirstOrDefault();
            ViewBag.Info = contact;
            ViewBag.Address = await _context.Addresses
                .Where(w => w.ContactID == contact.ContactID)
                .Include(i => i.Emails)
                .Include(i => i.Phones)
                .ToListAsync();
            ViewBag.Menu = await _context.Offers.ToListAsync();

            ViewBag.Logo = _context.Settings.Include(i => i.Logo).FirstOrDefault().Logo;
            ViewBag.Cookies = _context.Settings.FirstOrDefault().CookieInfo;
            ViewBag.TextHead = _context.Settings.FirstOrDefault().HeadText;
        }
    }
}
