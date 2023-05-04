using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace VDMJasminka.Data.Migrations.UserManagementMigrations
{
    public partial class Initial_User_Create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    username = table.Column<string>(nullable: false),
                    password_hash = table.Column<byte[]>(nullable: false),
                    password_salt = table.Column<byte[]>(nullable: false),
                    role = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("user_pkey", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "password_hash", "password_salt", "role", "username" },
                values: new object[] { 1, new byte[] { 172, 248, 49, 98, 103, 33, 243, 194, 73, 190, 48, 192, 79, 136, 229, 207, 84, 147, 179, 151, 151, 180, 54, 96, 146, 126, 44, 148, 115, 198, 147, 219, 152, 235, 168, 194, 186, 185, 122, 237, 144, 90, 40, 26, 1, 164, 216, 69, 30, 201, 253, 202, 234, 202, 78, 45, 190, 149, 179, 105, 34, 57, 223, 157 }, new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, "ADMIN", "filip@test.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
