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
    public class ContactController : AbstractController
    {
        public ContactController(ILogger<HomeController> logger, WebContext context)
        : base(logger, context)
        {
        }

        // GET: Create
        public IActionResult Create()
        {
            return RedirectToAction("Create", "Address");
        }

        // GET: Contact/Edit/5
        public async Task<IActionResult> Index()
        {

            Contact contact = await _context.Contacts.FirstOrDefaultAsync();
            if(contact == null)
            {
                _context.Contacts.Add(new Contact() { KRS="",Name="",NIP="",REGON=""});
                 await _context.SaveChangesAsync();
                contact = await _context.Contacts.FirstOrDefaultAsync();
            }

            ViewBag.Address = await _context.Addresses.Include(i => i.Phones).Include(i => i.Emails).Where(w => w.ContactID == contact.ContactID).ToListAsync();
            if (contact == null)
            {
                return NotFound();
            }
            return View("Edit",contact);
        }

        // POST: Contact/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("ContactID,Name,NIP,REGON,KRS")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists(contact.ContactID))
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
            return View(contact);
        }

        private bool ContactExists(int id)
        {
            return _context.Contacts.Any(e => e.ContactID == id);
        }

    }
}
