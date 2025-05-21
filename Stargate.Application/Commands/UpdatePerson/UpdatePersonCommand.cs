namespace Stargate.Application.Commands.UpdatePerson;

public record UpdatePersonCommand(
    string Name,
    string? CurrentRank,
    string? CurrentDutyTitle,
    DateTime? CareerStartDate,
    DateTime? CareerEndDate
) : IRequest<UpdatePersonResponse>;