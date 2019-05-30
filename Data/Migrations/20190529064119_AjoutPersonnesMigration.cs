using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Geneao.Data.Migrations
{
    public partial class AjoutPersonnesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Personne",
                columns: table => new
                {
                    Personne_Id = table.Column<Guid>(nullable: false),
                    EditDate = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    Famille_Id = table.Column<string>(nullable: false),
                    Prenom = table.Column<string>(nullable: true),
                    LieuNaissance = table.Column<string>(nullable: true),
                    DateNaissance = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personne", x => x.Personne_Id);
                    table.ForeignKey(
                        name: "FK_Personne_Famille_Famille_Id",
                        column: x => x.Famille_Id,
                        principalTable: "Famille",
                        principalColumn: "Famille_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Personne_Famille_Id",
                table: "Personne",
                column: "Famille_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Personne");
        }
    }
}
