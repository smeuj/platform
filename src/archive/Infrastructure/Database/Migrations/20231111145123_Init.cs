using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Smeuj.Platform.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PublicName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 400, nullable: false),
                    DiscordId = table.Column<ulong>(type: "INTEGER", nullable: true),
                    AuthorSince = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    Version = table.Column<int>(type: "INTEGER", rowVersion: true, nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Smeuj",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Value = table.Column<string>(type: "TEXT", nullable: false),
                    AuthorId = table.Column<int>(type: "INTEGER", nullable: false),
                    DiscordId = table.Column<ulong>(type: "INTEGER", nullable: false),
                    SubmittedOn = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    ProcessedOn = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    Version = table.Column<int>(type: "INTEGER", rowVersion: true, nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Smeuj", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Smeuj_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Examples",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Value = table.Column<string>(type: "TEXT", nullable: false),
                    SubmittedOn = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    ProcessedOn = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    SmeuId = table.Column<int>(type: "INTEGER", nullable: false),
                    Version = table.Column<int>(type: "INTEGER", rowVersion: true, nullable: false, defaultValue: 0),
                    AuthorId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examples", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Examples_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Examples_Smeuj_SmeuId",
                        column: x => x.SmeuId,
                        principalTable: "Smeuj",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inspirations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    SubmittedOn = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    ProcessedOn = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    SmeuId = table.Column<int>(type: "INTEGER", nullable: false),
                    AuthorId = table.Column<int>(type: "INTEGER", nullable: true),
                    Value = table.Column<string>(type: "TEXT", nullable: true),
                    Version = table.Column<int>(type: "INTEGER", rowVersion: true, nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inspirations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inspirations_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Inspirations_Smeuj_SmeuId",
                        column: x => x.SmeuId,
                        principalTable: "Smeuj",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Authors_DiscordId",
                table: "Authors",
                column: "DiscordId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Examples_AuthorId",
                table: "Examples",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Examples_SmeuId",
                table: "Examples",
                column: "SmeuId");

            migrationBuilder.CreateIndex(
                name: "IX_Inspirations_AuthorId",
                table: "Inspirations",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Inspirations_SmeuId",
                table: "Inspirations",
                column: "SmeuId");

            migrationBuilder.CreateIndex(
                name: "IX_Smeuj_AuthorId",
                table: "Smeuj",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Smeuj_DiscordId",
                table: "Smeuj",
                column: "DiscordId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Smeuj_Value",
                table: "Smeuj",
                column: "Value",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Examples");

            migrationBuilder.DropTable(
                name: "Inspirations");

            migrationBuilder.DropTable(
                name: "Smeuj");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
