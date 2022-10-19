using MatchOdds.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace MatchOdds.Domain.Models.Match
{
#pragma warning disable
    public class MatchAddModel
    {
        public string Description { get; set; }
        [Required]
        public string MatchDate { get; set; }
        [Required]
        public string MatchTime { get; set; }
        [MaxLength(10)]
        [Required]
        public string TeamA { get; set; }
        [MaxLength(10)]
        [Required]
        public string TeamB { get; set; }
        [Required]
        public SportType Sport { get; set; }
    }
}
