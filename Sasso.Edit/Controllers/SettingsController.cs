using Sald.Data.HelperClass;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sasso.Edit.Controllers.Abstract;
using Microsoft.Extensions.Logging;
using Sasso.Edit.Controllers;
using Sald.Data.Data.Data;
using Sasso.Data.Data;
using Sasso.Data.Data.Data;
using System.Collections.Generic;

namespace Sald.Edit.Controllers
{
    public class SettingsController : AbstractController
    {
        public SettingsController(ILogger<HomeController> logger, WebContext context)
        : base(logger, context)
        {
        }

        // GET: Settings/Edit/5
        public async Task<IActionResult> Index()
        {
            var settings = await _context.Settings.Include(i => i.Background).Include(i => i.Logo).FirstOrDefaultAsync();
            if (settings == null)
            {
                _context.Settings.Add(new Settings() 
                { 
                    CookieInfo = "Używamy informacji zapisanych za pomocą plików cookies w celu zapewnienia maksymalnej wygody w korzystaniu z naszego serwisu. Mogą też korzystać z nich współpracujące z nami firmy badawcze oraz reklamowe. Jeżeli wyrażasz zgodę na zapisywanie informacji zawartej w cookies kliknij na „x” w prawym górnym rogu tej informacji. Jeśli nie wyrażasz zgody, ustawienia dotyczące plików cookies możesz zmienić w swojej przeglądarce.",
                    HeadText = "Witamy!"
                });
                _context.SaveChanges();
            }
            var settingsSend = await _context.Settings.Include(i => i.Background).Include(i => i.Logo).FirstOrDefaultAsync();
            string path;
            if (settingsSend.Logo == null)
                path = "";
            else
                path = settingsSend.Logo.Path;

            return View(settingsSend);
        }

        // POST: Settings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("LogoForm,BackgroundForm")] Settings s, string path)
        {
            if (ModelState.IsValid)
            {
                var settingsDB = await _context.Settings.Include(i=>i.Logo).Include(i=>i.Background).FirstOrDefaultAsync();
                if (settingsDB == null)
                    return NotFound();
                try
                {
                    #region Logo-MyFile
                    //change
                    if(s.LogoForm != null && path != null)
                    {
                        var img = await _context.MyFiles.FirstOrDefaultAsync(f => f.Path == path);
                        img.Tag = "logo";
                        img.Path = FileAction.ChangeFile(path, s.LogoForm).Path;
                        _context.MyFiles.Update(img);
                    }
                    //new
                    else if(s.LogoForm != null)
                    {
                        settingsDB.Logo = FileAction.UploadFiles(s.LogoForm).Result.FirstOrDefault();
                        settingsDB.Logo.Tag = "logo";
                    }
                    #endregion

                    #region Background
                    if(s.BackgroundForm != null)
                    {
                        if (settingsDB.Background == null)
                            settingsDB.Background = new List<MyFile>();

                        foreach (var item in s.BackgroundForm)
                        {
                            settingsDB.Background.Add(new MyFile() { Path = FileAction.UploadFiles(item).Result.First().Path });
                        }
                    }

                    #endregion

                    _context.Settings.Update(settingsDB);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SettingsExists(s.SettingsID))
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
            return View(s);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cookie([Bind("CookieInfo")] Settings s)
        {
                try
                {
                    var settings = await _context.Settings.FirstOrDefaultAsync();
                    if(settings != null)
                    {
                        settings.CookieInfo = s.CookieInfo;
                        _context.Update(settings);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _context.Settings.FirstOrDefaultAsync() == null)
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HeadText([Bind("HeadText")] Settings s)
        {
                try
                {
                    var settings = await _context.Settings.FirstOrDefaultAsync();
                    if(settings != null)
                    {
                        settings.HeadText = s.HeadText;
                        _context.Update(settings);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _context.Settings.FirstOrDefaultAsync() == null)
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


        private bool AboutExists(int id)
        {
            return _context.Abouts.Any(e => e.AboutID == id);
        }

        private bool SettingsExists(int id)
        {
            return _context.Settings.Any(e => e.SettingsID == id);
        }
   

    #region JS
        public bool DeleteBackgroundJS(int id)
        {
            var list = _context.MyFiles;
            if (list.Count() == 1)
                return false;
            var file = list.FirstOrDefault(f => f.FileID == id);
            bool output;
            if (file != null)
            {
                FileAction.RemoveFile(file.Path);
                var rezult = _context.MyFiles.Remove(file);
                output = rezult.State.ToString() == "Deleted";
                _context.SaveChanges();
                return output;
            }

            return false;
        }

     #endregion
    }
}
