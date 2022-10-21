using MatchOdds.Domain.Interfaces;
using MatchOdds.Domain.Models.Match;
using MatchOdds.Domain.Models.Odd;
using Moq;

namespace MatchOdds.XUnitTests.Mocks
{
    internal class MockIOddRepository
    {
        public static Mock<IOddRepositoryService> GetMock()
        {
            var mock = new Mock<IOddRepositoryService>();
            var odds = new List<OddModel>()
            {
                new OddModel
                {
                    MatchId = 1,
                    Specifier = "1",
                    MatchOdd = 1.4
                },
                new OddModel
                {
                    MatchId = 1,
                    Specifier = "x",
                    MatchOdd = 2.3
                }
            };

            // Set up
            mock.Setup(x => x.GetAllMatchOdds().Result).Returns(() => odds);
            mock.Setup(x => x.GetMatchOddById(It.IsAny<int>()).Result).Returns((int id) => odds.FirstOrDefault(x => x.ID == id));
            mock.Setup(x => x.AddMatchOdd(It.IsAny<OddAddModel>()).Result).Callback(() => { return; });
            mock.Setup(x => x.UpdateMatchOdd(It.IsAny<OddUpdateModel>()).Result).Callback(() => { return; });
            mock.Setup(x => x.DeleteMatchOdd(It.IsAny<int>()).Result).Callback(() => { return; });
            return mock;
        }
    }
}
