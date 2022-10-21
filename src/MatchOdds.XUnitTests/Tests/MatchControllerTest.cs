using MatchOdds.Controllers;
using MatchOdds.Domain.Enums;
using MatchOdds.Domain.Models.Match;
using MatchOdds.XUnitTests.Mocks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MatchOdds.XUnitTests.Tests
{

    // Same procedure for Odds controller
    public class MatchControllerTest
    {
        [Fact]
        public async void WhenGettingAllMatches_ThenAllMatchesReturn()
        {
            var repositoryWrapperMock = MockIRepositoryWrapper.GetMock();
            var matchController = new MatchController(repositoryWrapperMock.Object);
            object obj = await matchController.Get();
            var result = obj as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.IsAssignableFrom<IEnumerable<MatchModel>>(result.Value);
            Assert.NotEmpty(result.Value as IEnumerable<MatchModel>);
        }

        [Fact]
        public async void GivenAnIdOfAnExistingMatch_WhenGettingMatchyId_ThenMatchReturns()
        {
            var repositoryWrapperMock = MockIRepositoryWrapper.GetMock();

            var ownerController = new MatchController(repositoryWrapperMock.Object);

            object obj = await ownerController.Get(1);
            var result = obj as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.IsAssignableFrom<MatchModel>(result.Value);
            Assert.NotNull(result.Value as MatchModel);
        }

        [Fact]
        public async void GivenValidRequest_WhenCreatingMatch_ThenCreatedReturns()
        {
            var repositoryWrapperMock = MockIRepositoryWrapper.GetMock();
            var ownerController = new MatchController(repositoryWrapperMock.Object);

            var owner = new MatchAddModel()
            {
                Description = "PAOK-Aris",
                MatchDate = "20/10/2022",
                MatchTime = "20:00",
                TeamA = "PAOK",
                TeamB = "Aris",
                Sport = SportType.Football,
            };
            object obj = await ownerController.Post(owner);

            var result = obj as ObjectResult;

            Assert.NotNull(result);
            Assert.IsAssignableFrom<CreatedAtRouteResult>(result);
            Assert.Equal((int)HttpStatusCode.Created, result!.StatusCode);
            Assert.Equal("OwnerById", (result as CreatedAtRouteResult)!.RouteName);
        }

    }
}
