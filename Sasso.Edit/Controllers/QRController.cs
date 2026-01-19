using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration;
using QRCoder;
using System.Drawing;
using System.IO;


public class QRController : Controller
{
    // url przychodzi z widoku
    public IActionResult Generate(string url)
    {
        if (string.IsNullOrEmpty(url))
        {
            // fallback, jeśli url nie został przekazany
            url = "https://example.com";
        }

        using (var qrGenerator = new QRCodeGenerator())
        using (var qrData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q))
        using (var qrCode = new QRCode(qrData))
        using (var bitmap = qrCode.GetGraphic(20))
        using (var ms = new MemoryStream())
        {
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return File(ms.ToArray(), "image/png");
        }
    }
}
