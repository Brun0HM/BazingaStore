using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BazingaStore.Migrations
{
    /// <inheritdoc />
    public partial class pedidos23 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Finalizado",
                table: "Pedidos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Finalizado",
                table: "Pedidos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
