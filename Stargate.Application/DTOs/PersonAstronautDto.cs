namespace Stargate.Application.DTOs;

public class PersonAstronautDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    // Embedded astronaut details
    public string CurrentRank { get; set; } = string.Empty;
    public string CurrentDutyTitle { get; set; } = string.Empty;
    public DateTime CareerStartDate { get; set; }
    public DateTime? CareerEndDate { get; set; }
}