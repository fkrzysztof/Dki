using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sasso.Data.Data;
using Sasso.Data.Data.Data;
using Sasso.Edit.Controllers.Abstract;

namespace Sasso.Edit.Controllers
{
    public class AddressController : AbstractController
    {
        public AddressController(ILogger<HomeController> logger, WebContext context)
        : base(logger, context)
        {
        }


        // GET: Address/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Address/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AddressID,Name,HeadOffice,Country,City,Street,Number,ZIPCode,ContactID")] Address address, string[] _namePhone, string[] _phoneNumber, string[] _emailAdress)
        {
            if (ModelState.IsValid)
            {
                address.ContactID = _context.Contacts.First().ContactID;
                address.Phones = new List<Phone>();
                address.Emails = new List<Email>();


                for (int i = 0; i < _emailAdress.Length; i++)
                {
                    if (!string.IsNullOrEmpty(_emailAdress[i]))
                        address.Emails.Add(new Email()
                        {
                            EmailAdress = _emailAdress[i]
                        });
                }

                for (int i = 0; i < _phoneNumber.Length; i++)
                {
                    if (!string.IsNullOrEmpty(_phoneNumber[i]) || !string.IsNullOrEmpty(_namePhone[i]))
                        address.Phones.Add(new Phone() 
                    {  
                        NamePhone = _namePhone[i],
                        PhoneNumber = _phoneNumber[i]
                    });
                }

                _context.Add(address);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Contact");
            }
            
            return View(address);
        }

        // GET: Address/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.Addresses.Include(i => i.Phones).Include(i => i.Emails).FirstAsync(f => f.AddressID == id);
            if (address == null)
            {
                return NotFound();
            }
            return View(address);
        }

        // POST: Address/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AddressID,Name,HeadOffice,Country,City,Street,Number,ZIPCode,ContactID")] Address address, string[] _namePhone, string[] _phoneNumber, string[] _emailAdress)
        {
            if (id != address.AddressID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    _context.RemoveRange(_context.Phones.Where(w => w.AddressID == id));
                    _context.RemoveRange(_context.Emails.Where(w => w.AddressID == id));
                    _context.SaveChanges();

                    var tempAddress = await _context.Addresses.Include(i => i.Emails).Include(i => i.Phones).FirstOrDefaultAsync(f => f.AddressID == id);
                    if (tempAddress == null)
                        return NotFound();

                    tempAddress.Name = address.Name;
                    tempAddress.HeadOffice = address.HeadOffice;
                    tempAddress.Country = address.Country;
                    tempAddress.City = address.City;
                    tempAddress.Street = address.Street;
                    tempAddress.Number = address.Number;
                    tempAddress.ZIPCode = address.ZIPCode;
                    

                    for (int i = 0; i < _emailAdress.Length; i++)
                    {
                        if(!string.IsNullOrEmpty(_emailAdress[i]))
                        tempAddress.Emails.Add(new Email()
                        {
                            EmailAdress = _emailAdress[i]
                        });
                    }

                    for (int i = 0; i < _phoneNumber.Length; i++)
                    {
                        if(!string.IsNullOrEmpty(_phoneNumber[i]) || !string.IsNullOrEmpty(_namePhone[i]))
                        tempAddress.Phones.Add(new Phone()
                        {
                            NamePhone = _namePhone[i],
                            PhoneNumber = _phoneNumber[i]
                        });
                    }

                    _context.Addresses.Update(tempAddress);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressExists(address.AddressID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Contact");
            }

            return View(address);
        }

        // GET: Address/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.Addresses.Include(i => i.Emails).Include(i => i.Phones).FirstOrDefaultAsync(m => m.AddressID == id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // POST: Address/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            _context.RemoveRange(_context.Phones.Where(w => w.AddressID == id));
            _context.RemoveRange(_context.Emails.Where(w => w.AddressID == id));
            _context.RemoveRange(_context.Addresses.Where(w => w.AddressID == id));

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Contact");
        }

        private bool AddressExists(int id)
        {
            return _context.Addresses.Any(e => e.AddressID == id);
        }
    }
}
