using MatchOdds.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatchOdds.Domain.Entities
{
    public class Odd : BaseEntity<int>
    {
        [Required]
        public int MatchId { get; set; }
        [Required]
        public Match Match { get; set; } = null!;
        [MaxLength(3)]
        public string Specifier { get; set; } = null!;
        [Required]
        public double MatchOdd { get; set; }
    }
}
