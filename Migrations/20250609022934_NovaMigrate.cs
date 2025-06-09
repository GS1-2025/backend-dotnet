using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gs_sensolux.Migrations
{
    /// <inheritdoc />
    public partial class NovaMigrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SSX_ITENS_PEDIDO_SSX_PEDIDOS_ID_PEDIDO",
                table: "SSX_ITENS_PEDIDO");

            migrationBuilder.AlterColumn<decimal>(
                name: "PRECO_UNITARIO",
                table: "SSX_PRODUTOS",
                type: "NUMBER(38,17)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "NUMBER");

            migrationBuilder.CreateIndex(
                name: "IX_SSX_ITENS_PEDIDO_SSX_PEDIDOS_ID_PEDIDO",
                table: "SSX_ITENS_PEDIDO",
                column: "SSX_PEDIDOS_ID_PEDIDO",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SSX_ITENS_PEDIDO_SSX_PEDIDOS_ID_PEDIDO",
                table: "SSX_ITENS_PEDIDO");

            migrationBuilder.AlterColumn<decimal>(
                name: "PRECO_UNITARIO",
                table: "SSX_PRODUTOS",
                type: "NUMBER",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "NUMBER(38,17)");

            migrationBuilder.CreateIndex(
                name: "IX_SSX_ITENS_PEDIDO_SSX_PEDIDOS_ID_PEDIDO",
                table: "SSX_ITENS_PEDIDO",
                column: "SSX_PEDIDOS_ID_PEDIDO");
        }
    }
}
