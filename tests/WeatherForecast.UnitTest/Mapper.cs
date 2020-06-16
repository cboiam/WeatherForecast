using WeatherForecast.Core.Mappings;

namespace WeatherForecast.UnitTest
{
    public static class Mapper
    {
        static Mapper()
        {
            Instance = new AutoMapper.MapperConfiguration(opt =>
                {
                    opt.AddProfile<DtoToEntityProfile>();
                }).CreateMapper();
        }

        public static AutoMapper.IMapper Instance { get; }
    }
}
