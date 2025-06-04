using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BazingaStore.Migrations
{
    /// <inheritdoc />
    public partial class pagamento2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_CuponsDesconto_CupomDescontoId",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_CupomDescontoId",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "CupomDescontoId",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "ValorDesconto",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "ValorOriginal",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "ValorTotal",
                table: "Pedidos");

            migrationBuilder.AddColumn<Guid>(
                name: "CarrinhoId",
                table: "Pagamentos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarrinhoId",
                table: "Pagamentos");

            migrationBuilder.AddColumn<Guid>(
                name: "CupomDescontoId",
                table: "Pedidos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorDesconto",
                table: "Pedidos",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorOriginal",
                table: "Pedidos",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorTotal",
                table: "Pedidos",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_CupomDescontoId",
                table: "Pedidos",
                column: "CupomDescontoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_CuponsDesconto_CupomDescontoId",
                table: "Pedidos",
                column: "CupomDescontoId",
                principalTable: "CuponsDesconto",
                principalColumn: "CupomDescontoId");
        }
    }
}
