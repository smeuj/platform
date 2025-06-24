using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Smeuj.Platform.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class SmeujRemovedUniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Smeuj_Value",
                table: "Smeuj");

            migrationBuilder.CreateIndex(
                name: "IX_Smeuj_Value",
                table: "Smeuj",
                column: "Value");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Smeuj_Value",
                table: "Smeuj");

            migrationBuilder.CreateIndex(
                name: "IX_Smeuj_Value",
                table: "Smeuj",
                column: "Value",
                unique: true);
        }
    }
}
