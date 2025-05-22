namespace Stargate.Application.DTOs;

public class PersonAstronautDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public bool IsAstronaut =>
        !string.IsNullOrWhiteSpace(CurrentRank) &&
        !string.IsNullOrWhiteSpace(CurrentDutyTitle) &&
        CareerStartDate != default;

    public string CurrentRank { get; set; } = string.Empty;
    public string CurrentDutyTitle { get; set; } = string.Empty;
    public DateTime? CareerStartDate { get; set; }
    public DateTime? CareerEndDate { get; set; }
}