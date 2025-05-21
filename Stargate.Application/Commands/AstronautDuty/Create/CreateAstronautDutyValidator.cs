namespace Stargate.Application.Commands.AstronautDuty.Create;

public class CreateAstronautDutyValidator : AbstractValidator<CreateAstronautDutyCommand>
{
    public CreateAstronautDutyValidator()
    {
        RuleFor(x => x.PersonId)
            .NotEmpty().WithMessage("PersonId is required");

        RuleFor(x => x.DutyTitle)
            .NotEmpty().WithMessage("DutyTitle is required")
            .MaximumLength(100);

        RuleFor(x => x.Rank)
            .NotEmpty().WithMessage("Rank is required");

        RuleFor(x => x.DutyStartDate)
            .NotEmpty().WithMessage("DutyStartDate is required");

        RuleFor(x => x.DutyEndDate)
            .Must((cmd, endDate) =>
            {
                if (endDate == null) return true;
                return endDate > cmd.DutyStartDate;
            }).WithMessage("DutyEndDate must be after DutyStartDate if provided");
    }
}