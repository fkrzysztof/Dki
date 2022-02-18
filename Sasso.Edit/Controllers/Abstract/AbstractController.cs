using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sasso.Data.Data;

namespace Sasso.Edit.Controllers.Abstract
{
    [Authorize]
    public class AbstractController : Controller
    {
        protected readonly ILogger<HomeController> _logger;
        protected readonly WebContext _context;

        public AbstractController(ILogger<HomeController> logger, WebContext context)
        {
            _logger = logger;
            _context = context;
        }
    }
}
