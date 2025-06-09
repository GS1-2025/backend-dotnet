using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gs_sensolux.Migrations
{
    /// <inheritdoc />
    public partial class NewMigrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PRECO_UNITARIO",
                table: "SSX_PRODUTOS",
                type: "NUMBER(38,17)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "NUMBER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PRECO_UNITARIO",
                table: "SSX_PRODUTOS",
                type: "NUMBER",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "NUMBER(38,17)");
        }
    }
}
