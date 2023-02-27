
using DatabaseTest.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseTest.Database.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder
               .ToTable("Product")
               .HasKey(product => product.Id);

            builder.HasOne<CheckEntity>(product => product.Check)
                    .WithMany(check => check.Products)
                    .HasForeignKey(product => product.CheckFK)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
