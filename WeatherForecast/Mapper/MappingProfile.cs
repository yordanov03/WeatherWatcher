using AutoMapper;
using System.Reflection;
using WeatherWatcher.Api.Dtos;
using WeatherWatcher.Models;

namespace WeatherWatcher.Api.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
           => this.ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            CreateMap<WeatherForecast, WeatherForecastDto>();
        }
    }
}
