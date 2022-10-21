using MatchOdds.Domain;
using Moq;

namespace MatchOdds.XUnitTests.Mocks;

internal class MockIRepositoryWrapper
{
    public static Mock<IUnitOfWork> GetMock()
    {
        var mock = new Mock<IUnitOfWork>();
        var ownerRepoMock = MockIMatchRepository.GetMock();
        var accountRepoMock = MockIOddRepository.GetMock();
        mock.Setup(m => m.MatchRepositoryService).Returns(() => ownerRepoMock.Object);
        mock.Setup(m => m.OddRepositoryServiceService).Returns(() => accountRepoMock.Object);
        mock.Setup(m => m.Commit()).Callback(() => { return; });
        return mock;
    }
}