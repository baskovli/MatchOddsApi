using MatchOdds.Domain.Enums;
using MatchOdds.Domain.Models.Match;
using System.ComponentModel.DataAnnotations;

namespace MatchOdds.XUnitTests;

public class ValidationTests
{
    [Theory]
    [InlineData(null, null, null, null, null, null, false)]
    [InlineData(null, null, null, "20/10/2022", null, null, false)]
    [InlineData(null, "PAOK", null, null, null, null, false)]
    [InlineData("PAOK-Aris", "PAOK", "Aris", "20/10/2022", "20:00", 1, true)]
    // get all cases 
    public void TestModelValidation(string description, string teamA, string teamB, string matchDate, string matchTime, int? sport, bool isValid)
    {
        var owner = new MatchAddModel()
        {
            Description = description,
            MatchDate = matchDate,
            MatchTime = matchTime,
            TeamA = teamA,
            TeamB = teamB,
            Sport = (SportType)sport,
        };

        Assert.Equal(isValid, ValidateModel(owner));
    }

    private bool ValidateModel(object model)
    {
        var validationResults = new List<ValidationResult>();
        var ctx = new ValidationContext(model, null, null);
        return Validator.TryValidateObject(model, ctx, validationResults, true);
    }
}