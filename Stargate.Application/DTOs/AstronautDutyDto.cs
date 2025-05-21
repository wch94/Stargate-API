namespace Stargate.Application.DTOs;

public class AstronautDutyDto
{
    public int Id { get; set; }
    public int PersonId { get; set; }
    public string Rank { get; set; } = string.Empty;
    public string DutyTitle { get; set; } = string.Empty;
    public DateTime DutyStartDate { get; set; }
    public DateTime? DutyEndDate { get; set; }
    public string PersonName { get; set; } = string.Empty;
}