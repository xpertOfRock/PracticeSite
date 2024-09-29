using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PracticeSite.Models.Entities;

namespace PracticeSite.Data.EntityConfigurations
{
    public class VacancyConfiguration : IEntityTypeConfiguration<Vacancy>
    {
        public void Configure(EntityTypeBuilder<Vacancy> builder)
        {
            builder.ToTable("Vacancies");
            builder.HasKey(v => v.Id);

            builder.Property(v => v.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(v => v.Description)
                .IsRequired();

            builder.Property(v => v.Salary)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(v => v.Status)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(v => v.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(v => v.ClosedAt)
                .IsRequired(false);

            builder.OwnsOne(a => a.Address);
        }
    }
}
