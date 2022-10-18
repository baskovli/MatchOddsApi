using MatchOdds.Domain.Models.Odd;

namespace MatchOdds.Domain.Models.Match
{
#pragma warning disable
    public class MatchModel
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string MatchDate { get; set; }
        public string MatchTime { get; set; }
        public string TeamA { get; set; }
        public string TeamB { get; set; }
        public string Sport { get; set; }

        public IList<OddModel> Odds { get; set; }

        public MatchModel()
        {
            Odds = new List<OddModel>();
        }
    }
}
