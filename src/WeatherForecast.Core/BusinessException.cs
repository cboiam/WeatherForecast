using System;

namespace WeatherForecast.Core
{
    public class BusinessException : Exception
    {
        public BusinessException(string message) 
            : base(message) { }
    }
}
