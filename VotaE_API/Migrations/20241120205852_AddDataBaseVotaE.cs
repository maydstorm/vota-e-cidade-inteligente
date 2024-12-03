using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VotaE_API.Migrations
{
    /// <inheritdoc />
    public partial class AddDataBaseVotaE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_Usuario",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Senha = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Telefone = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    UsuarioRole = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Usuario", x => x.UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "TB_Sugestao",
                columns: table => new
                {
                    SugestaoId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Descricao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Titulo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Localizacao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Observacao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "date", nullable: false),
                    UsuarioId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Sugestao", x => x.SugestaoId);
                    table.ForeignKey(
                        name: "FK_TB_Sugestao_TB_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "TB_Usuario",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_Projetos",
                columns: table => new
                {
                    ProjetoId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Descricao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Titulo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Status = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "date", nullable: false),
                    DataEnvio = table.Column<DateTime>(type: "date", nullable: false),
                    DataAprovacao = table.Column<DateTime>(type: "date", nullable: false),
                    SugestaoId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Projetos", x => x.ProjetoId);
                    table.ForeignKey(
                        name: "FK_TB_Projetos_TB_Sugestao_SugestaoId",
                        column: x => x.SugestaoId,
                        principalTable: "TB_Sugestao",
                        principalColumn: "SugestaoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_Projetos_SugestaoId",
                table: "TB_Projetos",
                column: "SugestaoId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Sugestao_UsuarioId",
                table: "TB_Sugestao",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_Projetos");

            migrationBuilder.DropTable(
                name: "TB_Sugestao");

            migrationBuilder.DropTable(
                name: "TB_Usuario");
        }
    }
}
