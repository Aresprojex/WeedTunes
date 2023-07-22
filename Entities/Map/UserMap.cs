using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using WeedTunes.Models.Constant;

namespace WeedTunes.Entities.Map
{
    public class UserMap : IEntityTypeConfiguration<ApplicationUser>
    {
        public PasswordHasher<ApplicationUser> Hasher { get; set; } = new PasswordHasher<ApplicationUser>();

        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            SetupUsers(builder);
        }

        private void SetupUsers(EntityTypeBuilder<ApplicationUser> builder)
        {
            var sysUser = new ApplicationUser
            {
                Id = Defaults.SysUserId,
                FirstName = "WeedTunes User",
                LastName = "User",
                Email = Defaults.SysUserEmail,
                EmailConfirmed = true,
                NormalizedEmail = Defaults.SysUserEmail.ToUpper(),
                PhoneNumber = Defaults.SysUserMobile,
                UserName = Defaults.SysUserEmail,
                NormalizedUserName = Defaults.SysUserEmail.ToUpper(),
                TwoFactorEnabled = false,
                PhoneNumberConfirmed = true,
                PasswordHash = Hasher.HashPassword(null, "optimA_1"),
                SecurityStamp = "99ae0c45-d682-4542-9ba7-1281e471916b",
            };

            builder.HasData(sysUser);
        }
    }
}
