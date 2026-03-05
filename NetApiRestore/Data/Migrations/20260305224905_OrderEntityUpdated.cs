using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetApiRestore.Data.Migrations
{
    /// <inheritdoc />
    public partial class OrderEntityUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "PaymentIntentId",
                keyValue: null,
                column: "PaymentIntentId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentIntentId",
                table: "Orders",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1314c3e5-eff9-45ad-9cfd-f4ba7ed656b9",
                column: "ConcurrencyStamp",
                value: "9d87a3fb-ea9a-433f-b8cc-028b135b2613");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1abc9de4-45e1-4900-9f79-be8db3f90e3c",
                column: "ConcurrencyStamp",
                value: "df50e6f8-47ee-412f-bf0a-f3990103fc88");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PaymentIntentId",
                table: "Orders",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1314c3e5-eff9-45ad-9cfd-f4ba7ed656b9",
                column: "ConcurrencyStamp",
                value: "4a6756d7-94f3-4020-b61d-6b41fbe38420");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1abc9de4-45e1-4900-9f79-be8db3f90e3c",
                column: "ConcurrencyStamp",
                value: "4017a053-5e46-48be-9ff4-1bf0a897d63a");
        }
    }
}
