using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using GloboClimaPlatform.Application.DTOs.User;
using GloboClimaPlatform.Core.UseCases.DynamoDb;
using GloboClimaPlatform.Core.UseCases.User;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GloboClimaPlatform.Controllers
{
    [Route("auth")]
    public class AuthController(
        IGetUserByEmailUseCase userByEmailUseCase,
        IGetUserByLoginCredentialsUseCase userByLoginCredentialsUseCase,
        IUserLoginUseCase userLoginUseCase,
        IUserRegisterUseCase userRegisterUseCase,
        IAddDynamoUserUseCase addDynamoUserUseCase) : Controller
    {
        [HttpGet("login")]
        public async Task<ActionResult> LoginView()
        {
            var jwt = HttpContext.Request.Cookies["JwtToken"];
            if (User.Identity is { IsAuthenticated: false } || jwt is null) return View("Login");

            var email = User.Claims.FirstOrDefault(item => item.Type.Equals(JwtRegisteredClaimNames.Email))!.Value
                .ToString();

            var user = await userByEmailUseCase.Execute(email);

            if (user is null) return View("Login");

            if (!jwt.Equals(user.JWT)) return View("Login");

            return RedirectToAction("HomeView", "Home");
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromForm] UserLoginDto userLoginDto)
        {
            var foundUser = await userByLoginCredentialsUseCase.Execute(userLoginDto);

            if (foundUser is not null)
            {
                await userLoginUseCase.Execute(foundUser);

                Response.Cookies.Append("JwtToken", foundUser.JWT, new CookieOptions
                {
                    HttpOnly = true, // Prevent JavaScript from accessing the cookie
                    Secure = true, // Ensure the cookie is sent over HTTPS only
                    SameSite = SameSiteMode.Strict, // Prevent the cookie from being sent with cross-site requests
                    Expires = DateTimeOffset.UtcNow.AddYears(5),
                });

                return RedirectToAction("HomeView", "Home");
            }

            TempData["Error"] = "Credenciais inválidas";
            return RedirectToAction("LoginView");
        }

        [HttpGet("register")]
        public ActionResult RegisterView()
        {
            var jwt = HttpContext.Request.Cookies["JwtToken"];
            if (User.Identity is { IsAuthenticated: false } || jwt is null) return View("Register");

            return RedirectToAction("HomeView", "Home");
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromForm] UserRegisterDto userRegisterDto)
        {
            try
            {
                var foundUser = await userByEmailUseCase.Execute(userRegisterDto.Email);

                if (foundUser is not null)
                {
                    TempData["Error"] = "Email já está em uso";
                    return RedirectToAction("RegisterView");
                }

                await addDynamoUserUseCase.Execute(new()
                {
                    PartitionKey = userRegisterDto.Email,
                    SortKey = userRegisterDto.Name
                });
                
                var newJwt = await userRegisterUseCase.Execute(userRegisterDto);

                Response.Cookies.Append("JwtToken", newJwt, new CookieOptions
                {
                    HttpOnly = true, // Prevent JavaScript from accessing the cookie
                    Secure = true, // Ensure the cookie is sent over HTTPS only
                    SameSite = SameSiteMode.Strict, // Prevent the cookie from being sent with cross-site requests
                    Expires = DateTimeOffset.UtcNow.AddYears(5),
                });
                
                return RedirectToAction("HomeView", "Home");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                TempData["Error"] = "Falha ao se cadastrar!";
                return RedirectToAction("RegisterView");
            }
        }
    }
}