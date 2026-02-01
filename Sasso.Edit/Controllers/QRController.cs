using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.Web.CodeGeneration;
using QRCoder;
using System;
using System.Drawing;
using System.IO;


//public class QRController : Controller
//{
//    // url przychodzi z widoku
//    public IActionResult Generate(string url)
//    {
//        if (string.IsNullOrEmpty(url))
//        {
//            // fallback, jeśli url nie został przekazany
//            url = "https://example.com";
//        }

//        using (var qrGenerator = new QRCodeGenerator())
//        using (var qrData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q))
//        using (var qrCode = new QRCode(qrData))
//        using (var bitmap = qrCode.GetGraphic(20))
//        using (var ms = new MemoryStream())
//        {
//            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
//            return File(ms.ToArray(), "image/png");
//        }
//    }
//}



public class QRController : Controller
{
    public IActionResult Generate(string url)
    {
        if (string.IsNullOrEmpty(url))
            return BadRequest();

        if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
            return BadRequest("Invalid URL");

        var scheme = Request.Scheme;
        var host = Request.Host.Value;

        var finalUrl = $"{scheme}://{host}{uri.PathAndQuery}";

        using var qrGenerator = new QRCodeGenerator();
        using var qrData = qrGenerator.CreateQrCode(finalUrl, QRCodeGenerator.ECCLevel.Q);
        using var qrCode = new QRCode(qrData);
        using var bitmap = qrCode.GetGraphic(20);
        using var ms = new MemoryStream();

        bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        return File(ms.ToArray(), "image/png");
    }

}


//public class QRController : Controller
//{
//    public IActionResult Generate(string url)
//    {
//        if (string.IsNullOrEmpty(url))
//            return BadRequest();
//        var scheme = Request.Scheme;
//        var ip = HttpContext.Connection.LocalIpAddress?.ToString();
//        var port = Request.Host.Port;

//        var uri = new Uri(url);

//        // port tylko jeśli istnieje
//        var portPart = port.HasValue ? $":{port}" : "";

//        var finalUrl = $"{scheme}://{ip}{portPart}{uri.PathAndQuery}";


//        using var qrGenerator = new QRCodeGenerator();
//        using var qrData = qrGenerator.CreateQrCode(finalUrl, QRCodeGenerator.ECCLevel.Q);
//        using var qrCode = new QRCode(qrData);
//        using var bitmap = qrCode.GetGraphic(20);
//        using var ms = new MemoryStream();

//        bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
//        return File(ms.ToArray(), "image/png");
//    }
//}



//public class QRController : Controller
//{
//    private readonly IConfiguration _config;

//    public QRController(IConfiguration config)
//    {
//        _config = config;
//    }

//    public IActionResult Generate(string url)
//    {
//        if (string.IsNullOrEmpty(url))
//            return BadRequest();

//        var uri = new Uri(url);
//        var scheme = Request.Scheme;

//        var useIp = _config.GetValue<bool>("QrSettings:UseIpInsteadOfDomain");
//        var forceIp = _config.GetValue<string>("QrSettings:ForceIp");
//        var forcePort = _config.GetValue<int?>("QrSettings:ForcePort");

//        string host;
//        string portPart = "";

//        if (useIp)
//        {
//            host = !string.IsNullOrEmpty(forceIp)
//                ? forceIp
//                : HttpContext.Connection.LocalIpAddress?.ToString();

//            if (host == null)
//                return BadRequest("Brak IP serwera");

//            var port = forcePort ?? Request.Host.Port;
//            if (port.HasValue)
//                portPart = $":{port}";
//        }
//        else
//        {
//            host = Request.Host.Host;
//            if (Request.Host.Port.HasValue)
//                portPart = $":{Request.Host.Port}";
//        }

//        var finalUrl = $"{scheme}://{host}{portPart}{uri.PathAndQuery}";

//        using var qrGenerator = new QRCodeGenerator();
//        using var qrData = qrGenerator.CreateQrCode(finalUrl, QRCodeGenerator.ECCLevel.Q);
//        using var qrCode = new QRCode(qrData);
//        using var bitmap = qrCode.GetGraphic(20);
//        using var ms = new MemoryStream();

//        bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
//        return File(ms.ToArray(), "image/png");
//    }
//}
