﻿namespace Stargate.Domain.Entities;

[Table("AstronautDuty")]
public class AstronautDuty
{
    public int Id { get; set; }

    public int PersonId { get; set; }

    public string Rank { get; set; } = string.Empty;

    public string DutyTitle { get; set; } = string.Empty;

    public DateTime DutyStartDate { get; set; }

    public DateTime? DutyEndDate { get; set; }

    public virtual required Person Person { get; set; }
}

public class AstronautDutyConfiguration : IEntityTypeConfiguration<AstronautDuty>
{
    public void Configure(EntityTypeBuilder<AstronautDuty> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
    }
}
