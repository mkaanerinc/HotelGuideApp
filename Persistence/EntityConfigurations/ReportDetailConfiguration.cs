using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfigurations;

public class ReportDetailConfiguration : IEntityTypeConfiguration<ReportDetail>
{
    public void Configure(EntityTypeBuilder<ReportDetail> builder)
    {
        builder.ToTable("ReportDetails").HasKey(r => r.Id);

        builder.Property(r => r.Id).HasColumnName("Id").IsRequired();
        builder.Property(r => r.ReportId).HasColumnName("ReportId").IsRequired();
        builder.Property(r => r.Location).HasColumnName("Location").IsRequired();
        builder.Property(r => r.HotelCount).HasColumnName("HotelCount").IsRequired();
        builder.Property(r => r.PhoneCount).HasColumnName("PhoneCount").IsRequired();
        builder.Property(r => r.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(r => r.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(r => r.DeletedDate).HasColumnName("DeletedDate");

        builder.HasOne(r => r.Report);

        builder.HasQueryFilter(r => !r.DeletedDate.HasValue);
    }
}
