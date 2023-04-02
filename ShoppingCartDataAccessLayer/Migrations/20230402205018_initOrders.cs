using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingCartDataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class initOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductNames",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductNames",
                table: "Orders");
        }
    }
}
