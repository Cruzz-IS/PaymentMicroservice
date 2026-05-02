using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentsSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsSystem.Infrastructure.Persistence.Configurations
{
    // Infrastructure/Persistence/Configurations/OrderConfiguration.cs
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            //builder.Property(o => o.TotalAmount)
            //    .HasColumnType("decimal(18,2)")
            //    .IsRequired();

            //builder.Property(o => o.Currency)
            //    .HasMaxLength(3)
            //    .IsRequired();

            //builder.Property(o => o.Status)
            //    .HasConversion<string>()
            //    .HasMaxLength(20)
            //    .IsRequired();

            //builder.Property(o => o.CreatedAt)
            //    .HasDefaultValueSql("GETUTCDATE()");

            //builder.HasMany(o => o.Items)
            //    .WithOne(i => i.Order)
            //    .HasForeignKey(i => i.OrderId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //builder.HasIndex(o => o.CustomerId);
            //builder.HasIndex(o => o.Status);
            builder.HasIndex(o => o.CreatedAt);
        }
    }
}
