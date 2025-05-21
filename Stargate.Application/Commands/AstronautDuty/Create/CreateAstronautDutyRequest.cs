namespace Stargate.Application.Commands.AstronautDuty.Create;

public class CreateAstronautDutyRequest
{
    public string Rank { get; set; } = string.Empty;
    public string DutyTitle { get; set; } = string.Empty;
    public DateTime DutyStartDate { get; set; }
    public DateTime? DutyEndDate { get; set; }
}