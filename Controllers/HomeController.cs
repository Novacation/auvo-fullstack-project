using System.Security.Claims;
using GloboClimaPlatform.Infra.ExternalApis.ResponsesDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Newtonsoft.Json;

namespace GloboClimaPlatform.Controllers;

[Authorize]
[Route("home")]
public class HomeController : Controller
{
    
    public IActionResult HomeView()
    {
        if (User.Identity is { IsAuthenticated: true })
        {
            ViewData["UserName"] = User.Claims.FirstOrDefault(item => item.Type.Equals(JwtRegisteredClaimNames.Name))!
                .Value.ToString();
            
            // Pass the country object to the view
            return View("Home");
        }

        TempData["Error"] = "Você não está logado!";
        return RedirectToAction("LoginView", "Auth");
    }
}