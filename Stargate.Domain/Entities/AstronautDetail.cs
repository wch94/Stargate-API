﻿//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using Microsoft.EntityFrameworkCore;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace Stargate.Domain.Entities
//{
//    [Table("AstronautDetail")]
//    public class AstronautDetail
//    {
//        public int Id { get; set; }

//        public int PersonId { get; set; }

//        public string CurrentRank { get; set; } = string.Empty;

//        public string CurrentDutyTitle { get; set; } = string.Empty;

//        public DateTime CareerStartDate { get; set; }

//        public DateTime? CareerEndDate { get; set; }

//        public virtual Person Person { get; set; }
//    }

//    public class AstronautDetailConfiguration : IEntityTypeConfiguration<AstronautDetail>
//    {
//        public void Configure(EntityTypeBuilder<AstronautDetail> builder)
//        {
//            builder.HasKey(x => x.Id);
//            builder.Property(x => x.Id).ValueGeneratedOnAdd();
//        }
//    }
//}
