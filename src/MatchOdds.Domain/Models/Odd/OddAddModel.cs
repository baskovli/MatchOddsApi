using System.ComponentModel.DataAnnotations;

namespace MatchOdds.Domain.Models.Match
{
    public class OddAddModel
    {
        [Required]
        public int MatchId { get; set; }
        [MaxLength(3)]
        public string Specifier { get; set; } = null!;
        [Required]
        public double MatchOdd { get; set; }
    }
}
