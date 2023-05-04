using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VDMJasminka.Core.Users;

namespace VDMJasminka.Data.EntityConfigurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> userConfiguration)
        {
            userConfiguration.ToTable("users");
            userConfiguration.HasKey(x => x.Id).HasName("user_pkey");
            userConfiguration.Property(x => x.Username).IsRequired();
            userConfiguration.Property(x => x.Role).IsRequired();
            userConfiguration.Property(x => x.PasswordHash).IsRequired();
            userConfiguration.Property(x => x.PasswordSalt).IsRequired();
            userConfiguration.Property(x => x.Id);
        }
    }
}
