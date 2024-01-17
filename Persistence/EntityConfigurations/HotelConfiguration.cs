using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfigurations;

public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
{
    public void Configure(EntityTypeBuilder<Hotel> builder)
    {
        builder.ToTable("Hotels").HasKey(h => h.Id);

        builder.Property(h => h.Id).HasColumnName("Id").IsRequired();
        builder.Property(h => h.ManagerFirstName).HasColumnName("ManagerFirstName").IsRequired();
        builder.Property(h => h.ManagerLastName).HasColumnName("ManagerLastName").IsRequired();
        builder.Property(h => h.CompanyName).HasColumnName("CompanyName").IsRequired();
        builder.Property(h => h.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(h => h.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(h => h.DeletedDate).HasColumnName("DeletedDate");

        builder.HasIndex(indexExpression: h => h.CompanyName, name:"UK_Hotels_CompanyName").IsUnique();

        builder.HasMany(h => h.ContactInformations);

        builder.HasQueryFilter(h => !h.DeletedDate.HasValue);
    }
}
