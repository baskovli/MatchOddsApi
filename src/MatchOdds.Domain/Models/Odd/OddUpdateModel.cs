using System.ComponentModel.DataAnnotations;

namespace MatchOdds.Domain.Models.Match
{
    public class OddUpdateModel : OddAddModel
    {
        public int ID { get; set; }
    }
}
