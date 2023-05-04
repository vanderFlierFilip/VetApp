using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System;
using VDMJasminka.Core.Users;
using VDMJasminka.Data.EntityConfigurations;

namespace VDMJasminka.Data
{
    public class UserManagementDbContext : DbContext
    {
        public UserManagementDbContext(DbContextOptions options) : base(options)
        {
        }
        public virtual DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.UseIdentityAlwaysColumns();
            builder.ApplyConfiguration(new UserEntityConfiguration());
            builder.Entity<User>().HasData(CreateAdmin(1, "filip@test.com", "opensesame"));
        }

        private static User CreateAdmin(int id, string name, string password)
        {
            var admin = new User();

            byte[] salt = new byte[128 / 8];
            using var hmac = new HMACSHA512(salt);
            admin.Id = id;
            admin.Username = name;
            admin.PasswordSalt = salt;
            admin.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            admin.Role = "ADMIN";

            return admin;
        }

    }
}
