using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GloboClimaPlatform.Controllers;

[Route("home")]
public class HomeController : Controller
{
    [Authorize]
    public IActionResult HomeView()
    {
        var name = User.Claims.FirstOrDefault(item => item.Type.Equals(ClaimTypes.Name));

        if (name is not null) return View("Home");
        TempData["Error"] = "Você não está logado!";
        return RedirectToAction("LoginView", "Auth");
    }
}