namespace Stargate.Application.Commands.AstronautDuty.Create;

public record CreateAstronautDutyCommand(
    int PersonId,
    string Rank,
    string DutyTitle,
    DateTime DutyStartDate,
    DateTime? DutyEndDate
) : IRequest<CreateAstronautDutyResponse>;