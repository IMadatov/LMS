using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Auth.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedNavigationItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "NavigationItemId",
                table: "AspNetRoles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "NavigationItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Commmand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hidden = table.Column<bool>(type: "bit", nullable: false),
                    NavigationItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NavigationItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NavigationItems_NavigationItems_NavigationItemId",
                        column: x => x.NavigationItemId,
                        principalTable: "NavigationItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_NavigationItemId",
                table: "AspNetRoles",
                column: "NavigationItemId");

            migrationBuilder.CreateIndex(
                name: "IX_NavigationItems_NavigationItemId",
                table: "NavigationItems",
                column: "NavigationItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_NavigationItems_NavigationItemId",
                table: "AspNetRoles",
                column: "NavigationItemId",
                principalTable: "NavigationItems",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_NavigationItems_NavigationItemId",
                table: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "NavigationItems");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_NavigationItemId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "NavigationItemId",
                table: "AspNetRoles");
        }
    }
}
