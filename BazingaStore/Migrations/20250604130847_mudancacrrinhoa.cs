using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BazingaStore.Migrations
{
    /// <inheritdoc />
    public partial class mudancacrrinhoa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Finalizado",
                table: "Carrinhos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Finalizado",
                table: "Carrinhos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
