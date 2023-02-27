
using DatabaseTest.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseTest.Database.Configurations
{
    public class BuyerConfiguration : IEntityTypeConfiguration<BuyerEntity>
    {
        public void Configure(EntityTypeBuilder<BuyerEntity> builder)
        {
            builder
              .ToTable("Buyer")
              .HasKey(buyer => buyer.Id);
        }
    }
}
