using GloboClimaPlatform.Adapters.ExternalApis.Countries;
using GloboClimaPlatform.Application.DTOs.Countries;
using GloboClimaPlatform.Application.DTOs.DynamoDb;
using GloboClimaPlatform.Core.UseCases.DynamoDb;
using GloboClimaPlatform.Infra.ExternalApis.ResponsesDTOs;
using GloboClimaPlatform.Infra.Repositories.DynamoDb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Newtonsoft.Json;

namespace GloboClimaPlatform.Controllers;

[Authorize]
[Route("countries")]
public class CountriesController(IGetCountryByNameService countryByNameService, IFavoriteCountryUseCase favoriteCountryUseCase, IGetFavoriteCountriesUseCase favoriteCountriesUseCase) : Controller
{
    [HttpPost]
    public async Task<IActionResult> GetCountryByName([FromForm] GetCountryByNameDto countryByNameDto)
    {
        try
        {
            var foundCountry = await countryByNameService.Execute(countryByNameDto);

            TempData["Country"] = JsonConvert.SerializeObject(foundCountry);
        }
        catch (HttpRequestException)
        {
            TempData["Error"] = "Erro ao buscar informações do país";
        }

        return RedirectToAction("CountriesView");
    }
    
    [HttpGet]
    public IActionResult CountriesView()
    {
        if (User.Identity is { IsAuthenticated: true })
        {
            ViewData["UserName"] = User.Claims.FirstOrDefault(item => item.Type.Equals(JwtRegisteredClaimNames.Name))!
                .Value.ToString();
            if (TempData["Country"] == null) return View("Countries");
            // Deserialize the JSON string back into the DTO object
            var country =
                JsonConvert.DeserializeObject<CountryByNameApiResponseDto>(TempData["Country"].ToString());

            // Pass the country object to the view
            return View("Countries", country);
        }

        TempData["Error"] = "Você não está logado!";
        return RedirectToAction("LoginView", "Auth");
    }
    
    [HttpPost("favorite")]
    public async Task<IActionResult> FavoriteCountry([FromForm] FavoriteCountryDto favoriteCountryDto)
    {
        if (User.Identity is { IsAuthenticated: false }) return RedirectToAction("LoginView", "Auth");
        
        var email = User.Claims.FirstOrDefault(item => item.Type.Equals(JwtRegisteredClaimNames.Email))!.Value
            .ToString();
        
        var userName = User.Claims.FirstOrDefault(item => item.Type.Equals(JwtRegisteredClaimNames.Name))!.Value
            .ToString();

        await favoriteCountryUseCase.Execute(new GloboClimaPlatformDynamoDbModel
        {
            PartitionKey = email,
            SortKey = userName,
        }, favoriteCountryDto.CountryName);
        
        TempData["SuccessMsg"] = "País favoritado com sucesso!";
        return RedirectToAction("CountriesView");
    }
    
    [HttpGet("favorite-countries")]
    public async Task<IActionResult> FavoriteCountriesView([FromForm] FavoriteCityDto favoriteCityDto)
    {
        if (User.Identity is { IsAuthenticated: false }) return RedirectToAction("LoginView", "Auth");
        
        var email = User.Claims.FirstOrDefault(item => item.Type.Equals(JwtRegisteredClaimNames.Email))!.Value
            .ToString();
        
        var userName = User.Claims.FirstOrDefault(item => item.Type.Equals(JwtRegisteredClaimNames.Name))!.Value
            .ToString();
        ViewData["UserName"] = userName;
        
        var favoriteCountries = await favoriteCountriesUseCase.Execute(email, userName);
        
        return View("FavoriteCountries", favoriteCountries);
    }
}