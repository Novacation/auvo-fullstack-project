using GloboClimaPlatform.Application.DTOs.Countries;
using GloboClimaPlatform.Infra.ExternalApis.ResponsesDTOs;

namespace GloboClimaPlatform.Adapters.ExternalApis.Countries;

public interface IGetCountryByNameService
{
    public Task<CountryByNameApiResponseDto?> Execute(GetCountryByNameDto countryByNameDto);
}