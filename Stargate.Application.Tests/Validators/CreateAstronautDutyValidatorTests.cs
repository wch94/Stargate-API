using FluentValidation.TestHelper;
using Stargate.Application.Commands.AstronautDuty.Create;

namespace Stargate.Application.Tests.Validators;

public class CreateAstronautDutyValidatorTests
{
    private readonly CreateAstronautDutyValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_Name_Is_Empty()
    {
        var model = new CreateAstronautDutyCommand
        (
            PersonId: 1, // Fixed required parameter
            Rank: "Commander",
            DutyTitle: "",
            DutyStartDate: DateTime.Today,
            DutyEndDate: null // Optional parameter
        );

        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.DutyTitle);
    }
}