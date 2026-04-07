using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetApiRestore.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCouponRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_AppCoupon_CouponId",
                table: "Baskets");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1314c3e5-eff9-45ad-9cfd-f4ba7ed656b9",
                column: "ConcurrencyStamp",
                value: "b2c4fb39-cf9d-4371-99f7-37b88cbbaa84");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1abc9de4-45e1-4900-9f79-be8db3f90e3c",
                column: "ConcurrencyStamp",
                value: "0781490b-df94-4a0b-9bc4-ec01437fbc18");

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_AppCoupon_CouponId",
                table: "Baskets",
                column: "CouponId",
                principalTable: "AppCoupon",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_AppCoupon_CouponId",
                table: "Baskets");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_AppCoupon_CouponId",
                table: "Baskets",
                column: "CouponId",
                principalTable: "AppCoupon",
                principalColumn: "Id");
        }
    }
}
