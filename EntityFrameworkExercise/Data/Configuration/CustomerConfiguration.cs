using EntityFrameworkExercise.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkExercise.Data.Configuration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e.IsDeleted);

            builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);

            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}
