namespace GloboClimaPlatform.Infra.ExternalApis.ResponsesDTOs;

public class CountryByNameApiResponseDto
{
    public NameDto Name { get; set; }
    public Dictionary<string, CurrenciesDto> Currencies;
    public List<string> Capital { get; set; }
    public int Population { get; set; }
    public Dictionary<string, string> Languages;
}

public class NameDto
{
    public string Common { get; set; }
}

public class CountryNameDto
{
    public NameDto Name { get; set; }
}

public class CurrenciesDto
{
    public string Name { get; set; }
    public string Symbol { get; set; }
}