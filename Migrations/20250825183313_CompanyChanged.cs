using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class CompanyChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discussions_Companies_CompanyId",
                table: "Discussions");

            migrationBuilder.DropIndex(
                name: "IX_Discussions_CompanyId",
                table: "Discussions");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Discussions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Discussions_OrderId",
                table: "Discussions",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Discussions_Order_OrderId",
                table: "Discussions",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discussions_Order_OrderId",
                table: "Discussions");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Discussions_OrderId",
                table: "Discussions");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Discussions");

            migrationBuilder.CreateIndex(
                name: "IX_Discussions_CompanyId",
                table: "Discussions",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Discussions_Companies_CompanyId",
                table: "Discussions",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }
    }
}
