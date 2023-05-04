using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using VDMJasminka.Data.Queries;

namespace VDMJasminka.Data.Migrations
{
    public partial class Create_InventoryViewTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(MedicamentInventoryQueries.CreateInventoryView);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(MedicamentInventoryQueries.DropInventoryView);
        }
    }
}