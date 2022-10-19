using AutoMapper;
using MatchOdds.Domain.Entities;
using MatchOdds.Domain.Models.Match;
using MatchOdds.Domain.Models.Odd;

namespace MatchOdds.Domain.AutoMapper
{
    public class OddMapper
    {
        public static void Set(IMapperConfigurationExpression cfg)
        {
            // Mapping from Entity to Api model
            cfg.CreateMap<Odd, OddModel>()
               .ForMember(dest => dest.Match, src => src.MapFrom(s => $"{s.Match.TeamA}-{s.Match.TeamB}"));

            // Mapping from Api model to Entity 
            cfg.CreateMap<OddModel, Odd>();
            cfg.CreateMap<OddAddModel, Odd>();
            cfg.CreateMap<MatchUpdateModel, Odd>();
        }
    }
}
