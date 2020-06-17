namespace WeatherForecast.Core.Dtos
{
    public class LocationDto : LocationResumeDto
    {
        public string LattLong { get; set; }
        public string LocationType { get; set; }
    }
}
