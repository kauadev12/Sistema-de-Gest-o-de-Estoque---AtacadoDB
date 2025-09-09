using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sistema_de_Gestão_de_Estoque.Migrations
{
    /// <inheritdoc />
    public partial class AddColunasUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_produtos_Fornecedores_FornecedorId",
                table: "produtos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_produtos",
                table: "produtos");

            migrationBuilder.RenameTable(
                name: "produtos",
                newName: "Produtos");

            migrationBuilder.RenameIndex(
                name: "IX_produtos_FornecedorId",
                table: "Produtos",
                newName: "IX_Produtos_FornecedorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Produtos",
                table: "Produtos",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Senha = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataCriacao = table.Column<DateOnly>(type: "date", nullable: false),
                    Telefone = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataNascimento = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Fornecedores_FornecedorId",
                table: "Produtos",
                column: "FornecedorId",
                principalTable: "Fornecedores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Fornecedores_FornecedorId",
                table: "Produtos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Produtos",
                table: "Produtos");

            migrationBuilder.RenameTable(
                name: "Produtos",
                newName: "produtos");

            migrationBuilder.RenameIndex(
                name: "IX_Produtos_FornecedorId",
                table: "produtos",
                newName: "IX_produtos_FornecedorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_produtos",
                table: "produtos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_produtos_Fornecedores_FornecedorId",
                table: "produtos",
                column: "FornecedorId",
                principalTable: "Fornecedores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
