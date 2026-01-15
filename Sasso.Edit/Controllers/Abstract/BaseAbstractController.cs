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
            var contact = _context.Contacts.FirstOrDefault();
            ViewBag.Info = contact;
            ViewBag.Address = await _context.Addresses
                .Where(w => w.ContactID == contact.ContactID)
                .Include(i => i.Emails)
                .Include(i => i.Phones)
                .ToListAsync();
            ViewBag.Logo = _context.Settings.Include(i => i.Logo).FirstOrDefault().Logo;
            ViewBag.Cookies = _context.Settings.FirstOrDefault().CookieInfo;
            ViewBag.TextHead = _context.Settings.FirstOrDefault().HeadText;
            ViewBag.Offer = await _context.Offers.Include(i => i.Image).ToListAsync();
            var projectsList = await _context.Projects.Where(w => w.Active == true && DateTime.Compare(w.StartProject, DateTime.Now) <= 0 &&
                                            DateTime.Compare(w.EndProject, DateTime.Now) >= 0).ToListAsync();
            if (projectsList.Count() == 0)
                projectsList = await _context.Projects.Where(w => w.Active == true && DateTime.Compare(w.EndProject, DateTime.Now) < 0).ToListAsync();
            ViewBag.Projects = projectsList;
        }
    }
}
