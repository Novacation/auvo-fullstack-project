using GloboClimaPlatform.Adapters.ExternalApis.OpenWeatherMap;
using GloboClimaPlatform.Application.DTOs.Cities;
using GloboClimaPlatform.Application.DTOs.DynamoDb;
using GloboClimaPlatform.Core.UseCases.DynamoDb;
using GloboClimaPlatform.Infra.ExternalApis.ResponsesDTOs.OpenWeather;
using GloboClimaPlatform.Infra.Repositories.DynamoDb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Newtonsoft.Json;

namespace GloboClimaPlatform.Controllers;

[Authorize]
[Route("cities")]
public class CitiesController(IGetClimateDataByCityNameService climateDataByCityNameService, IFavoriteCityUseCase favoriteCityUseCase, IGetFavoriteCitiesUseCase favoriteCitiesUseCase) : Controller
{
    [HttpGet]
    public IActionResult CitiesView()
    {
        if (User.Identity is { IsAuthenticated: true })
        {
            ViewData["UserName"] = User.Claims.FirstOrDefault(item => item.Type.Equals(JwtRegisteredClaimNames.Name))!
                .Value.ToString();
            if (TempData["City"] == null) return View("Cities");
            // Deserialize the JSON string back into the DTO object
            var climateDataByCityName =
                JsonConvert.DeserializeObject<GetClimateDataByCityNameApiResponseDto>(TempData["City"].ToString());

            // Pass the country object to the view
            return View("Cities", climateDataByCityName);
        }
        else
        {
            return RedirectToAction("LoginView", "Auth");
        }
    }

    [HttpPost("city-climate-data")]
    public async Task<IActionResult> GetClimateDataByCityName(
        [FromForm] GetClimateDataByCityNameDto climateDataByCityNameDto)
    {
        try
        {
            var foundCity = await climateDataByCityNameService.Execute(climateDataByCityNameDto.CityName);

            TempData["City"] = JsonConvert.SerializeObject(foundCity);
        }
        catch (HttpRequestException)
        {
            TempData["Error"] = "Erro ao buscar informações climáticas da cidade";
        }

        return RedirectToAction("CitiesView");
    }

    [HttpPost("favorite")]
    public async Task<IActionResult> FavoriteCity([FromForm] FavoriteCityDto favoriteCityDto)
    {
        if (User.Identity is { IsAuthenticated: false }) return RedirectToAction("LoginView", "Auth");
        
        var email = User.Claims.FirstOrDefault(item => item.Type.Equals(JwtRegisteredClaimNames.Email))!.Value
            .ToString();
        
        var userName = User.Claims.FirstOrDefault(item => item.Type.Equals(JwtRegisteredClaimNames.Name))!.Value
            .ToString();

        await favoriteCityUseCase.Execute(new GloboClimaPlatformDynamoDbModel
        {
            PartitionKey = email,
            SortKey = userName,
        }, favoriteCityDto.CityName);
        
        TempData["SuccessMsg"] = "Cidade favoritada com sucesso!";
        return RedirectToAction("CitiesView");
    }
    
    [HttpGet("favorite-cities")]
    public async Task<IActionResult> FavoriteCitiesView([FromForm] FavoriteCityDto favoriteCityDto)
    {
        if (User.Identity is { IsAuthenticated: false }) return RedirectToAction("LoginView", "Auth");
        
        var email = User.Claims.FirstOrDefault(item => item.Type.Equals(JwtRegisteredClaimNames.Email))!.Value
            .ToString();
        
        var userName = User.Claims.FirstOrDefault(item => item.Type.Equals(JwtRegisteredClaimNames.Name))!.Value
            .ToString();
        ViewData["UserName"] = userName;
        
        var favoriteCities = await favoriteCitiesUseCase.Execute(email, userName);
        
        return View("FavoriteCities", favoriteCities);
    }
}