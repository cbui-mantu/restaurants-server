using Domain.Restaurants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Restaurants;

internal sealed class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
{
    public void Configure(EntityTypeBuilder<Restaurant> builder)
    {
        builder.ToTable("restaurants");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .HasColumnName("id");

        builder.Property(r => r.Name)
            .HasColumnName("name")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(r => r.Description)
            .HasColumnName("description")
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(r => r.Category)
            .HasColumnName("category")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(r => r.HasDelivery)
            .HasColumnName("has_delivery")
            .IsRequired();

        builder.Property(r => r.ContactEmail)
            .HasColumnName("contact_email")
            .HasMaxLength(255);

        builder.Property(r => r.ContactNumber)
            .HasColumnName("contact_number")
            .HasMaxLength(20);

        builder.OwnsOne(r => r.Address, addressBuilder =>
        {
            addressBuilder.Property(a => a.City)
                .HasColumnName("address_city")
                .HasMaxLength(100);

            addressBuilder.Property(a => a.Street)
                .HasColumnName("address_street")
                .HasMaxLength(200);

            addressBuilder.Property(a => a.PostalCode)
                .HasColumnName("address_postal_code")
                .HasMaxLength(20);
        });

        builder.HasMany(r => r.Dishes)
            .WithOne()
            .HasForeignKey("RestaurantId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
