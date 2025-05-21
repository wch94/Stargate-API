namespace Stargate.Application.Commands.Person.Create;

public record CreatePersonCommand(
    string Name,
    string? CurrentRank,
    string? CurrentDutyTitle,
    DateTime? CareerStartDate,
    DateTime? CareerEndDate
) : IRequest<CreatePersonResponse>;
