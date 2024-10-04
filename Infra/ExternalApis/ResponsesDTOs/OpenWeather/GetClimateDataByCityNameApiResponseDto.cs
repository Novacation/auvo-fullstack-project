namespace GloboClimaPlatform.Infra.ExternalApis.ResponsesDTOs.OpenWeather;

public class GetClimateDataByCityNameApiResponseDto
{
    public string Name { get; set; }
    public List<WeatherDto> Weather { get; set; }
    public MainDto Main { get; set; }
}

public class WeatherDto
{
    public string Main { get; set; }
    public string Description { get; set; }
}

public class MainDto
{
    public double Temp { get; set; }
    public double Feels_Like { get; set; }
    public double Temp_Min { get; set; }
    public double Temp_Max { get; set; }
    public int Pressure { get; set; }
    public int Humidity { get; set; }
    public int Sea_Level { get; set; }
    public int Grnd_Level { get; set; }
}