using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetApiRestore.Data.Migrations
{
    /// <inheritdoc />
    public partial class BasketEntityUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientSecret",
                table: "Baskets",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PaymentIntentId",
                table: "Baskets",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1314c3e5-eff9-45ad-9cfd-f4ba7ed656b9",
                column: "ConcurrencyStamp",
                value: "017395cb-fd80-4718-abad-4a5f8dcf612d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1abc9de4-45e1-4900-9f79-be8db3f90e3c",
                column: "ConcurrencyStamp",
                value: "4c0d4852-5b03-4ba3-8775-373d9b498f9b");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientSecret",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "PaymentIntentId",
                table: "Baskets");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1314c3e5-eff9-45ad-9cfd-f4ba7ed656b9",
                column: "ConcurrencyStamp",
                value: "8b4a6e4f-e6f4-4bd5-971a-935cb96981ba");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1abc9de4-45e1-4900-9f79-be8db3f90e3c",
                column: "ConcurrencyStamp",
                value: "481ddf43-6a15-4132-9397-da1357d0a908");
        }
    }
}
