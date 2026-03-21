using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Product;

internal class ProductsConfiguration : IEntityTypeConfiguration<ProductsDbModel>
{
    public void Configure(EntityTypeBuilder<ProductsDbModel> builder)
    {
        builder.ToTable("products");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();
    }
}
