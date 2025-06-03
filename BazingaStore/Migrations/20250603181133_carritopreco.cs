using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BazingaStore.Migrations
{
    /// <inheritdoc />
    public partial class carritopreco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrecoUnitario",
                table: "CarrinhosItens");

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhosItens_ProdutoId",
                table: "CarrinhosItens",
                column: "ProdutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarrinhosItens_Produtos_ProdutoId",
                table: "CarrinhosItens",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "ProdutoId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarrinhosItens_Produtos_ProdutoId",
                table: "CarrinhosItens");

            migrationBuilder.DropIndex(
                name: "IX_CarrinhosItens_ProdutoId",
                table: "CarrinhosItens");

            migrationBuilder.AddColumn<decimal>(
                name: "PrecoUnitario",
                table: "CarrinhosItens",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
