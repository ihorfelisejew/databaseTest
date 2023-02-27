
using DatabaseTest.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace DatabaseTest.Database.Configurations
{
    public class CheckConfiguration : IEntityTypeConfiguration<CheckEntity>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CheckEntity> builder)
        {
            builder
                .ToTable("Check")
            .HasKey(check => check.Id);

            builder.HasOne<BuyerEntity>(check => check.Buyer)
                    .WithMany(buyer => buyer.Checks)
                    .HasForeignKey(check => check.BuyerFK)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
