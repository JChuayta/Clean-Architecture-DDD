using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
                new ApplicationUser
                {
                    Id = "123123",
                    Email = "admin@localhost.com",
                    NormalizedEmail = "admin@localhost.com",
                    Name = "admin",
                    LastName = "localhost",
                    UserName = "jcmamani",
                    NormalizedUserName = "jcmamani",
                    PasswordHash = hasher.HashPassword(null, "viernes13"),
                    EmailConfirmed = true,
                },
                new ApplicationUser
                {
                    Id = "oijasd7887",
                    Email = "jchuayta@localhost.com",
                    NormalizedEmail = "jchuayta@localhost.com",
                    Name = "Carlos",
                    LastName = "Huayta",
                    UserName = "jchuayta",
                    NormalizedUserName = "jchuayta",
                    PasswordHash = hasher.HashPassword(null, "viernes13"),
                    EmailConfirmed = true,
                }
            );
        }
    }
}
