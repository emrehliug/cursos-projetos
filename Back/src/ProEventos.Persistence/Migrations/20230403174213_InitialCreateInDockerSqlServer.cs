using Microsoft.EntityFrameworkCore.Migrations;

namespace ProEventos.Persistence.Migrations
{
    public partial class InitialCreateInDockerSqlServer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RedeSocials_Eventos_EventoId",
                table: "RedeSocials");

            migrationBuilder.DropForeignKey(
                name: "FK_RedeSocials_Palestrantes_PalestranteId",
                table: "RedeSocials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RedeSocials",
                table: "RedeSocials");

            migrationBuilder.RenameTable(
                name: "RedeSocials",
                newName: "RedeSociais");

            migrationBuilder.RenameIndex(
                name: "IX_RedeSocials_PalestranteId",
                table: "RedeSociais",
                newName: "IX_RedeSociais_PalestranteId");

            migrationBuilder.RenameIndex(
                name: "IX_RedeSocials_EventoId",
                table: "RedeSociais",
                newName: "IX_RedeSociais_EventoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RedeSociais",
                table: "RedeSociais",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RedeSociais_Eventos_EventoId",
                table: "RedeSociais",
                column: "EventoId",
                principalTable: "Eventos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RedeSociais_Palestrantes_PalestranteId",
                table: "RedeSociais",
                column: "PalestranteId",
                principalTable: "Palestrantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RedeSociais_Eventos_EventoId",
                table: "RedeSociais");

            migrationBuilder.DropForeignKey(
                name: "FK_RedeSociais_Palestrantes_PalestranteId",
                table: "RedeSociais");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RedeSociais",
                table: "RedeSociais");

            migrationBuilder.RenameTable(
                name: "RedeSociais",
                newName: "RedeSocials");

            migrationBuilder.RenameIndex(
                name: "IX_RedeSociais_PalestranteId",
                table: "RedeSocials",
                newName: "IX_RedeSocials_PalestranteId");

            migrationBuilder.RenameIndex(
                name: "IX_RedeSociais_EventoId",
                table: "RedeSocials",
                newName: "IX_RedeSocials_EventoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RedeSocials",
                table: "RedeSocials",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RedeSocials_Eventos_EventoId",
                table: "RedeSocials",
                column: "EventoId",
                principalTable: "Eventos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RedeSocials_Palestrantes_PalestranteId",
                table: "RedeSocials",
                column: "PalestranteId",
                principalTable: "Palestrantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
