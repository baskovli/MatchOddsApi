#pragma warning disable

namespace MatchOdds.Domain.Models.Odd
{
    public class OddModel
    {
        public int ID { get; set; }
        public int MatchId { get; set; }
        public string Match { get; set; }
        public string Specifier { get; set; }
        public double Odd { get; set; }
    }
}
