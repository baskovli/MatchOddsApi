using AutoMapper;
using MatchOdds.Domain.Entities;
using MatchOdds.Domain.Models.Match;

namespace MatchOdds.Domain.AutoMapper
{
    public static class MatchMapper
    {
        public static void Set(IMapperConfigurationExpression cfg)
        {
            // Mapping from Entity to Api model
            cfg.CreateMap<Match, MatchModel>()
               .ForMember(dest => dest.Odds, src => src.MapFrom(s => s.Odds)); ;

            // Mapping from Api model to Entity 
            cfg.CreateMap<MatchModel, Match>();
            cfg.CreateMap<OddAddModel, Match>();
            cfg.CreateMap<MatchUpdateModel, Match>();
        }
    }
}
