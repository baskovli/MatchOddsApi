using MatchOdds.Domain.Enums;
using MatchOdds.Domain.Interfaces;
using MatchOdds.Domain.Models.Match;
using MatchOdds.Domain.Models.Odd;
using Moq;

namespace MatchOdds.XUnitTests.Mocks;

internal class MockIMatchRepository
{
    public static Mock<IMatchRepositoryService> GetMock()
    {
        var mock = new Mock<IMatchRepositoryService>();
        var matches = new List<MatchModel>()
        {
            new MatchModel
            {
                Description = "PAOK-Aris",
                MatchDate = "20/10/2022",
                MatchTime = "20:00",
                TeamA = "PAOK",
                TeamB = "Aris",
                Sport = "Football",
                Odds = new List<OddModel>
                {
                    new OddModel
                    {
                        Specifier = "1",
                        MatchOdd = 1.4
                    }
                }
            },
        };

        // Set up
        mock.Setup(x => x.GetAllMatches().Result).Returns(() => matches);
        mock.Setup(x => x.GetMatchById(It.IsAny<int>()).Result).Returns((int id) => matches.FirstOrDefault(x => x.ID == id));
        mock.Setup(x => x.AddMatch(It.IsAny<MatchAddModel>()).Result).Callback(() => { return; });
        mock.Setup(x => x.UpdateMatch(It.IsAny<MatchUpdateModel>()).Result).Callback(() => { return; });
        mock.Setup(x => x.DeleteMatch(It.IsAny<int>()).Result).Callback(() => { return; });
        return mock;
    }
}

