using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "lancamento");

            migrationBuilder.CreateTable(
                name: "T_NATUREZA",
                schema: "lancamento",
                columns: table => new
                {
                    PK_NATUREZA = table.Column<Guid>(type: "uuid", nullable: false),
                    TX_DESCRICAO = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CD_NATUREZA = table.Column<string>(type: "text", nullable: false),
                    DT_CADASTRO = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_NATUREZA", x => x.PK_NATUREZA);
                });

            migrationBuilder.CreateTable(
                name: "T_TIPO_LANCAMENTO",
                schema: "lancamento",
                columns: table => new
                {
                    PK_TP_LANCAMENTO = table.Column<Guid>(type: "uuid", nullable: false),
                    FK_NATUREZA = table.Column<Guid>(type: "uuid", nullable: false),
                    TX_DESCRICAO = table.Column<string>(type: "text", nullable: false),
                    CD_TIPO_LANCAMENTO = table.Column<string>(type: "text", nullable: false),
                    DT_CADASTRO = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_TIPO_LANCAMENTO", x => x.PK_TP_LANCAMENTO);
                    table.ForeignKey(
                        name: "FK_T_TIPO_LANCAMENTO_T_NATUREZA_FK_NATUREZA",
                        column: x => x.FK_NATUREZA,
                        principalSchema: "lancamento",
                        principalTable: "T_NATUREZA",
                        principalColumn: "PK_NATUREZA",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_LANCAMENTO",
                schema: "lancamento",
                columns: table => new
                {
                    PK_LANCAMENTO = table.Column<Guid>(type: "uuid", nullable: false),
                    FK_TP_LANCAMENTO = table.Column<Guid>(type: "uuid", nullable: false),
                    VALOR = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    NUM_LANCAMENTO = table.Column<Guid>(type: "uuid", nullable: false),
                    DT_CADASTRO = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_LANCAMENTO", x => x.PK_LANCAMENTO);
                    table.ForeignKey(
                        name: "FK_T_LANCAMENTO_T_TIPO_LANCAMENTO_FK_TP_LANCAMENTO",
                        column: x => x.FK_TP_LANCAMENTO,
                        principalSchema: "lancamento",
                        principalTable: "T_TIPO_LANCAMENTO",
                        principalColumn: "PK_TP_LANCAMENTO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "lancamento",
                table: "T_NATUREZA",
                columns: new[] { "PK_NATUREZA", "CD_NATUREZA", "DT_CADASTRO", "TX_DESCRICAO" },
                values: new object[,]
                {
                    { new Guid("08a6ac05-1f1c-41cb-adc2-2113820719e0"), "DEBITO", new DateTime(2025, 4, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Débito" },
                    { new Guid("cc934f0b-7ac0-43c4-9d50-7c2fc4b03d72"), "CREDITO", new DateTime(2025, 4, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Crédito" }
                });

            migrationBuilder.InsertData(
                schema: "lancamento",
                table: "T_TIPO_LANCAMENTO",
                columns: new[] { "PK_TP_LANCAMENTO", "CD_TIPO_LANCAMENTO", "DT_CADASTRO", "TX_DESCRICAO", "FK_NATUREZA" },
                values: new object[,]
                {
                    { new Guid("04d97159-bcf6-4cfb-b271-64917b800125"), "VENDA", new DateTime(2025, 4, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Venda", new Guid("cc934f0b-7ac0-43c4-9d50-7c2fc4b03d72") },
                    { new Guid("94c0f65a-661f-4656-beac-8bff9f8ea7f6"), "COMPRA", new DateTime(2025, 4, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Compra", new Guid("08a6ac05-1f1c-41cb-adc2-2113820719e0") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_LANCAMENTO_FK_TP_LANCAMENTO",
                schema: "lancamento",
                table: "T_LANCAMENTO",
                column: "FK_TP_LANCAMENTO");

            migrationBuilder.CreateIndex(
                name: "IX_T_TIPO_LANCAMENTO_FK_NATUREZA",
                schema: "lancamento",
                table: "T_TIPO_LANCAMENTO",
                column: "FK_NATUREZA");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_LANCAMENTO",
                schema: "lancamento");

            migrationBuilder.DropTable(
                name: "T_TIPO_LANCAMENTO",
                schema: "lancamento");

            migrationBuilder.DropTable(
                name: "T_NATUREZA",
                schema: "lancamento");
        }
    }
}
