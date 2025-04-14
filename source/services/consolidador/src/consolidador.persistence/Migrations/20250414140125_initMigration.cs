using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Consolidador.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "consolidado");

            migrationBuilder.CreateTable(
                name: "T_SALDO_DIARIO",
                schema: "consolidado",
                columns: table => new
                {
                    PK_SALDO_DIARIO = table.Column<Guid>(type: "uuid", nullable: false),
                    DT_SALDO = table.Column<DateTime>(type: "date", nullable: false),
                    VALOR = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    DT_CADASTRO = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_SALDO_DIARIO", x => x.PK_SALDO_DIARIO);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_SALDO_DIARIO",
                schema: "consolidado");
        }
    }
}
