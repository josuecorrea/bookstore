using BookStore.Domain;
using System.Data.Entity.ModelConfiguration;

namespace BookStore.Data.Mapping
{
    public class AuthorMap : EntityTypeConfiguration<Author>
    {
        public AuthorMap()
        {
            ToTable("Author");
            HasKey(c => c.Id);
            Property(c => c.FirstName).HasMaxLength(60).IsRequired();
            Property(c => c.LastName).HasMaxLength(60).IsRequired();
        }
    }
}