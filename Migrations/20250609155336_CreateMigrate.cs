
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gs_sensolux.Migrations
{
    /// <inheritdoc />
    public partial class CreateMigrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SSX_ENDERECOS",
                columns: table => new
                {
                    ID_ENDERECO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    SSX_USUARIOS_ID_USUARIO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CEP = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: false),
                    ESTADO = table.Column<string>(type: "NVARCHAR2(2)", maxLength: 2, nullable: false),
                    CIDADE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    BAIRRO = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    RUA = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SSX_ENDERECOS", x => x.ID_ENDERECO);
                    table.ForeignKey(
                        name: "SSX_USUARIOS_ID_USUARIO",
                        column: x => x.SSX_USUARIOS_ID_USUARIO,
                        principalTable: "SSX_USUARIOS",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SSX_PEDIDOS",
                columns: table => new
                {
                    ID_PEDIDO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    DATA_PEDIDO = table.Column<DateTime>(type: "DATE", nullable: false),
                    PRECO = table.Column<decimal>(type: "NUMBER(6,2)", nullable: false),
                    STATUS = table.Column<string>(type: "NVARCHAR2(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SSX_PEDIDOS", x => x.ID_PEDIDO);
                });

            migrationBuilder.CreateTable(
                name: "SSX_ITENS_PEDIDO",
                columns: table => new
                {
                    ID_ITEM_PEDIDO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    SSX_PEDIDOS_ID_PEDIDO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    SSX_USUARIOS_ID_USUARIO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    QUANTIDADE = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SSX_ITENS_PEDIDO", x => x.ID_ITEM_PEDIDO);
                    table.ForeignKey(
                        name: "FK_SSX_ITENS_PEDIDO_SSX_PEDIDOS_SSX_PEDIDOS_ID_PEDIDO",
                        column: x => x.SSX_PEDIDOS_ID_PEDIDO,
                        principalTable: "SSX_PEDIDOS",
                        principalColumn: "ID_PEDIDO",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SSX_ITENS_PEDIDO_SSX_USUARIOS_SSX_USUARIOS_ID_USUARIO",
                        column: x => x.SSX_USUARIOS_ID_USUARIO,
                        principalTable: "SSX_USUARIOS",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SSX_PRODUTOS",
                columns: table => new
                {
                    ID_PRODUTO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    SSX_IP_ID_ITEM_PEDIDO = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    NOME = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    DESCRICAO = table.Column<string>(type: "NVARCHAR2(80)", maxLength: 80, nullable: false),
                    PRECO_UNITARIO = table.Column<decimal>(type: "NUMBER(38,17)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SSX_PRODUTOS", x => x.ID_PRODUTO);
                    table.ForeignKey(
                        name: "FK_SSX_PRODUTOS_SSX_ITENS_PEDIDO_SSX_IP_ID_ITEM_PEDIDO",
                        column: x => x.SSX_IP_ID_ITEM_PEDIDO,
                        principalTable: "SSX_ITENS_PEDIDO",
                        principalColumn: "ID_ITEM_PEDIDO");
                });

            migrationBuilder.CreateTable(
                name: "SSX_SENSORES",
                columns: table => new
                {
                    ID_SENSOR = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    SSX_PRODUTOS_ID_PRODUTO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TIPO = table.Column<string>(type: "NVARCHAR2(12)", maxLength: 12, nullable: false),
                    DESCRICAO = table.Column<string>(type: "NVARCHAR2(40)", maxLength: 40, nullable: false),
                    MODELO = table.Column<string>(type: "NVARCHAR2(35)", maxLength: 35, nullable: false),
                    STATUS = table.Column<string>(type: "NVARCHAR2(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SSX_SENSORES", x => x.ID_SENSOR);
                    table.ForeignKey(
                        name: "FK_SSX_SENSORES_SSX_PRODUTOS_SSX_PRODUTOS_ID_PRODUTO",
                        column: x => x.SSX_PRODUTOS_ID_PRODUTO,
                        principalTable: "SSX_PRODUTOS",
                        principalColumn: "ID_PRODUTO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SSX_ENDERECOS_SSX_USUARIOS_ID_USUARIO",
                table: "SSX_ENDERECOS",
                column: "SSX_USUARIOS_ID_USUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_SSX_ITENS_PEDIDO_SSX_PEDIDOS_ID_PEDIDO",
                table: "SSX_ITENS_PEDIDO",
                column: "SSX_PEDIDOS_ID_PEDIDO",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SSX_ITENS_PEDIDO_SSX_USUARIOS_ID_USUARIO",
                table: "SSX_ITENS_PEDIDO",
                column: "SSX_USUARIOS_ID_USUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_SSX_PRODUTOS_SSX_IP_ID_ITEM_PEDIDO",
                table: "SSX_PRODUTOS",
                column: "SSX_IP_ID_ITEM_PEDIDO");

            migrationBuilder.CreateIndex(
                name: "IX_SSX_SENSORES_SSX_PRODUTOS_ID_PRODUTO",
                table: "SSX_SENSORES",
                column: "SSX_PRODUTOS_ID_PRODUTO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SSX_ENDERECOS");

            migrationBuilder.DropTable(
                name: "SSX_SENSORES");

            migrationBuilder.DropTable(
                name: "SSX_PRODUTOS");

            migrationBuilder.DropTable(
                name: "SSX_ITENS_PEDIDO");

            migrationBuilder.DropTable(
                name: "SSX_PEDIDOS");
        }
    }
}
