using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using global::PaymentsSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PaymentsSystem.Infrastructure.Persistence.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasDefaultValueSql("NEWSEQUENTIALID()"); 

            builder.Property(p => p.Amount)
                .HasColumnType("decimal(18,2)")          
                .IsRequired();

            builder.Property(p => p.Currency)
                .HasMaxLength(3)
                .IsRequired();

            builder.Property(p => p.Status)
                .HasConversion<string>()               
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(p => p.Provider)
                .HasConversion<string>()
                .HasMaxLength(20);

            //builder.Property(p => p.ExternalTransactionId)
            //    .HasMaxLength(100);

            //builder.Property(p => p.CreatedAt)
            //    .HasDefaultValueSql("GETUTCDATE()");   

            //builder.Property(p => p.UpdatedAt)
            //    .IsRequired(false);

            //// Índices para consultas frecuentes
            //builder.HasIndex(p => p.ExternalTransactionId)
            //    .IsUnique()
            //    .HasFilter("[ExternalTransactionId] IS NOT NULL"); // SQL Server: índice filtrado

            //builder.HasIndex(p => p.Status);
            //builder.HasIndex(p => p.CreatedAt);

            //// Relación con Order
            //builder.HasOne(p => p.Order)
            //    .WithMany(o => o.Payments)
            //    .HasForeignKey(p => p.OrderId)
            //    .OnDelete(DeleteBehavior.Restrict); // No borrar pagos en cascada
        }
    }
}
