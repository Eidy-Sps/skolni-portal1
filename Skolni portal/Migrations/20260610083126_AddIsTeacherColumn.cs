using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skolni_portal.Migrations
{
    /// <inheritdoc />
    public partial class AddIsTeacherColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TeacherCodes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Code", "CreatedAt" },
                values: new object[] { "UCITEL2026", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TeacherCodes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Code", "CreatedAt" },
                values: new object[] { "UCITEL2024", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
