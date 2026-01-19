using Engine.Data.Services;
using Engine.Edit.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sald.Data.Data.Data;
using Sald.Data.HelperClass;
using Sasso.Data.Data;
using Sasso.Edit.Controllers;
using Sasso.Edit.Controllers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Engine.Edit.Controllers
{
    public class ApartmentsController : AbstractController
    {
        private readonly EmailService _emailService;
        public ApartmentsController(ILogger<HomeController> logger, WebContext context, EmailService emailService)
        : base(logger, context)
        {
            _emailService = emailService;
        }



        // GET: Apartments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Apartments.Include(i => i.Photos).ToListAsync());
        }

        // Ta akcja jest dostępna dla wszystkich, nawet niezalogowanych
        [AllowAnonymous]
        // GET: Apartments
        public async Task<IActionResult> Show(string idName, int id)
        {
            var apartments = await _context.Apartments.Include(i => i.Photos).FirstOrDefaultAsync(f => f.Nazwa == idName && f.ApartmentID == id);
            if (apartments == null)
            {
                return NotFound(); // lub przekierowanie na stronę błędu
            }
            return View(apartments);
        }


        // GET: Apartments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Apartments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: Apartments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(
    "ApartmentID,Nazwa,Opis,Pietro,LiczbaPieterWBudynku,Metraz,LiczbaPokoi," +
    "WcRazemZLazienka,Balkon,Winda,Piwnica,OgrzewaniePodlogowe,Klimatyzacja," +
    "Garaz,MiejsceParkingoweNaZewnatrz,Ogrod,Taras," +
    "Ulica,NumerBudynku,NumerMieszkania,Miasto,KodPocztowy,Kraj," +
    "Email,Telefon1,Telefon2")] Apartment apartment, IFormFile[] FormFileItems)
        {
            if (!ModelState.IsValid)
                return View(apartment);

            // ===============================
            // 1. Dodaj Apartament do bazy
            // ===============================
            _context.Add(apartment);
            await _context.SaveChangesAsync(); // zapis, aby uzyskać ApartmentID dla zdjęć

            // ===============================
            // 2. Obsługa zdjęć
            // ===============================
            if (FormFileItems != null && FormFileItems.Any())
            {
                var files = await FileAction.UploadFiles(FormFileItems);
                foreach (var file in files)
                {
                    file.ApartmentID = apartment.ApartmentID;
                    _context.MyFiles.Add(file);
                }

                await _context.SaveChangesAsync();
            }

            // ===============================
            // 3. Przekierowanie do listy
            // ===============================
            return RedirectToAction(nameof(Index));
        }



        // GET: Apartments/Edit/5
        public IActionResult Edit(int id)
        {
            var apartment = _context.Apartments.Where(w => w.ApartmentID == id).Include(i => i.Photos).FirstOrDefault();
            if (apartment == null) return NotFound();

            //var model = new Apartment
            //{
            //    Apartment = apartment,
            //    ExistingImages = GetImagePaths(apartment) // np. z folderu UploadFile
            //};

            return View(apartment);
        }

        // POST: Apartments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApartmentID,Opis,Nazwa,Pietro,LiczbaPieterWBudynku,Metraz,LiczbaPokoi,WcRazemZLazienka,Balkon,Winda,Piwnica,OgrzewaniePodlogowe,Klimatyzacja,Garaz,MiejsceParkingoweNaZewnatrz,Ogród,Taras,Ulica,NumerBudynku,NumerMieszkania,Miasto,KodPocztowy,Kraj,Email,Telefon1,Telefon2")] Apartment apartment)
        {
            if (id != apartment.ApartmentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(apartment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApartmentExists(apartment.ApartmentID))
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
            return View(apartment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePhoto(int id)
        {
            var photo = await _context.MyFiles.FindAsync(id);
            if (photo == null)
                return Json(new { success = false });

            FileAction.RemoveFile(photo);
            _context.MyFiles.Remove(photo);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        ////DODAWANIE ZDJEC AJAX
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPhotos(int apartmentId, IFormFile[] files)
        {
            if (files == null || files.Length == 0)
                return BadRequest(new { success = false });

            var apartment = await _context.Apartments
                                          .Include(a => a.Photos)
                                          .FirstOrDefaultAsync(a => a.ApartmentID == apartmentId);

            if (apartment == null)
                return NotFound();

            // zapis plików
            var uploadedFiles = await FileAction.UploadFiles(files);

            // dodanie do apartamentu
            foreach (var f in uploadedFiles)
                apartment.Photos.Add(f);

            await _context.SaveChangesAsync();

            // zwracamy listę plików z URL i ID
            var result = uploadedFiles.Select(f => new
            {
                fileId = f.FileID,
                url = FileAction.GetImg(f)
            });

            return Json(new { success = true, files = result });
        }


        //USUWANIE APARTAMENTU I ZDJEC
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            // Znajdź apartament w bazie wraz ze zdjęciami
            var apartment = await _context.Apartments
                .Include(a => a.Photos) // wczytujemy powiązane zdjęcia
                .FirstOrDefaultAsync(a => a.ApartmentID == id);

            if (apartment == null)
                return NotFound();

            // Usuń wszystkie pliki z dysku
            if (apartment.Photos != null && apartment.Photos.Any())
            {
                FileAction.RemoveFile(apartment.Photos);
            }

            // Usuń rekordy zdjęć z bazy
            _context.MyFiles.RemoveRange(apartment.Photos);

            // Usuń apartament
            _context.Apartments.Remove(apartment);

            await _context.SaveChangesAsync();

            // Możesz zwrócić redirect albo JSON jeśli chcesz AJAX
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult SendMail(string Name, string Email, string Phone, string Message, string id)
        {
            // Tu logika wysyłki maila np. przez SmtpClient lub SendGrid
            // Możesz też zapisać do bazy jako lead

            TempData["Success"] = "Dziękujemy, Twoja wiadomość została wysłana!";
            //return RedirectToAction("Apartment", new { id = someApartmentId });
            return RedirectToAction("Show", new { id = id });
            
        }

        [HttpPost]
        public IActionResult SendMailAjax(ApartmentMailModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Proszę wypełnić wszystkie wymagane pola." });

            // Znajdź apartament w bazie
            var apartment = _context.Apartments.Find(model.ApartmentId);
            if (apartment == null || string.IsNullOrEmpty(apartment.Email))
                return Json(new { success = false, message = "Nie znaleziono apartamentu lub adresu email." });

            try
            {
                string body = $@"
                <p><strong>Imię:</strong> {model.Name}</p>
                <p><strong>Email:</strong> {model.Email}</p>
                <p><strong>Telefon:</strong> {model.Phone}</p>
                <p><strong>Wiadomość:</strong><br/>{model.Message}</p>
                <p><strong>Apartament:</strong> {apartment.Nazwa} (ID: {apartment.ApartmentID})</p>
                <p><strong>Apartament:</strong> {apartment.PelnyAdres}</p>

            ";

                _emailService.SendEmail(apartment.Email, "Zapytanie o apartament", body);

                return Json(new { success = true, message = "Dziękujemy, Twoja wiadomość została wysłana!" });
            }
            catch
            {
                return Json(new { success = false, message = "Wystąpił błąd przy wysyłce maila." });
            }
        }






        private bool ApartmentExists(int id)
        {
            return _context.Apartments.Any(e => e.ApartmentID == id);
        }
    }
}
