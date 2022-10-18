using MatchOdds.Domain.Common;
using MatchOdds.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace MatchOdds.Domain.Entities
{
#pragma warning disable
    public class Match : BaseEntity<int>
    {
        public Match()
        {
            Odds = new List<Odd>();
        }

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

        public ICollection<Odd> Odds { get; set; }
    }
}
