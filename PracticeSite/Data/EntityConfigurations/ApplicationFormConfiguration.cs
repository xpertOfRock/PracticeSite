using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PracticeSite.Models.Entities;
using PracticeSite.Models.Enums;

namespace PracticeSite.Data.EntityConfigurations
{
    public class ApplicationFormConfiguration : IEntityTypeConfiguration<ApplicationForm>
    {
        public void Configure(EntityTypeBuilder<ApplicationForm> builder)
        {
            builder.ToTable("Applications");

            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder
                .Property(x => x.Status)
                .HasDefaultValue(ApplicationStatus.InProgress)
                .IsRequired();

            builder
                .Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder
                .Property(x => x.Age)
                .IsRequired();

            builder
                .Property(x => x.PhoneNumber)
                .IsRequired();

            builder
                .Property(x => x.Email)
                .IsRequired();

            builder
                .Property(x => x.LandArea)
                .IsRequired();

            builder
                .Property(x => x.PricePerHectare)
                .IsRequired()
                .HasColumnType("decimal(18,2)");


            builder.OwnsOne(a => a.Address);

            builder.Property(a => a.Status)
                   .HasConversion<string>();
        }
    }
}
