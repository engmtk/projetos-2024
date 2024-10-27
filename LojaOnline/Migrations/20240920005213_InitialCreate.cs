using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaOnline.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    CpfCnpj = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CodigoProduto = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NomeCompleto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataEntrada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataSaida = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NomeProduto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CpfCnpjBaixa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Encerrado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => new { x.CpfCnpj, x.CodigoProduto });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
