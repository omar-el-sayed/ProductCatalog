using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.BL.Consts;

namespace ProductCatalog.PL.Controllers
{
    [Authorize(Roles = $"{Privilege.Admin},{Privilege.User}")]
    public class HomeController : Controller
    {
        [Authorize(Roles = Privilege.Admin)]
        public IActionResult Index() => View();

        public new IActionResult NotFound() => View();
    }
}