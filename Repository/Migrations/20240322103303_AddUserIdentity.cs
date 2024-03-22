using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateAt",
                value: new DateTimeOffset(new DateTime(2024, 3, 22, 10, 33, 3, 166, DateTimeKind.Unspecified).AddTicks(6737), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateAt",
                value: new DateTimeOffset(new DateTime(2024, 3, 21, 10, 33, 3, 166, DateTimeKind.Unspecified).AddTicks(6740), new TimeSpan(0, 0, 0, 0, 0)));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateAt",
                value: new DateTimeOffset(new DateTime(2024, 3, 22, 2, 10, 33, 56, DateTimeKind.Unspecified).AddTicks(7874), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateAt",
                value: new DateTimeOffset(new DateTime(2024, 3, 21, 2, 10, 33, 56, DateTimeKind.Unspecified).AddTicks(7878), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
