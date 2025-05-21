namespace Stargate.Application.Commands.Person.Update;

public record UpdatePersonCommand(
    string Name,
    string? CurrentRank,
    string? CurrentDutyTitle,
    DateTime? CareerStartDate,
    DateTime? CareerEndDate
) : IRequest<UpdatePersonResponse>;