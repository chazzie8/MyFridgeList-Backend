using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFridgeListWebapi.Migrations
{
    public partial class ItemEntityRelationshipAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Label = table.Column<string>(nullable: true),
                    Bought = table.Column<bool>(nullable: false),
                    ShoppinglistId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Shoppinglists_ShoppinglistId",
                        column: x => x.ShoppinglistId,
                        principalTable: "Shoppinglists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_ShoppinglistId",
                table: "Items",
                column: "ShoppinglistId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");
        }
    }
}
