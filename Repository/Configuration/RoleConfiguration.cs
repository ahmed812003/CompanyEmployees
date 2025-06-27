using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = new Guid("80abbca8-664d-4b20-b5de-024705497d4a").ToString(),
                    Name = "Manager",
                    NormalizedName = "MANAGER"
                },
                new IdentityRole
                {
                    Id = new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a").ToString(),
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                }
            );
        }
    }
}
