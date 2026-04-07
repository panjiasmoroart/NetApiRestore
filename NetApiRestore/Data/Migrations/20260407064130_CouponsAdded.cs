using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetApiRestore.Data.Migrations
{
    /// <inheritdoc />
    public partial class CouponsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CouponId",
                table: "Baskets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppCoupon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AmountOff = table.Column<long>(type: "bigint", nullable: true),
                    PercentOff = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true),
                    PromotionCode = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CouponId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCoupon", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1314c3e5-eff9-45ad-9cfd-f4ba7ed656b9",
                column: "ConcurrencyStamp",
                value: "8680069d-8808-46a8-8feb-d2152d7c9112");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1abc9de4-45e1-4900-9f79-be8db3f90e3c",
                column: "ConcurrencyStamp",
                value: "24461b16-d37a-4906-a0d5-626ad46976c5");

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_CouponId",
                table: "Baskets",
                column: "CouponId");

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_AppCoupon_CouponId",
                table: "Baskets",
                column: "CouponId",
                principalTable: "AppCoupon",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_AppCoupon_CouponId",
                table: "Baskets");

            migrationBuilder.DropTable(
                name: "AppCoupon");

            migrationBuilder.DropIndex(
                name: "IX_Baskets_CouponId",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "CouponId",
                table: "Baskets");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1314c3e5-eff9-45ad-9cfd-f4ba7ed656b9",
                column: "ConcurrencyStamp",
                value: "3c481992-3194-4c53-bb49-612929c7e6c5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1abc9de4-45e1-4900-9f79-be8db3f90e3c",
                column: "ConcurrencyStamp",
                value: "fa89b0bb-0553-4654-aaf0-b6f19b6875d5");
        }
    }
}
