using AutoMapper;

namespace MatchOdds.Domain.AutoMapper
{
    public static class AutoMapperSettings
    {
        public static IMapperConfigurationExpression AddMappings(
           this IMapperConfigurationExpression configurationExpression)
        {
            MatchMapper.Set(configurationExpression);
            OddMapper.Set(configurationExpression);

            return configurationExpression;
        }
    }
}
