using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hogar_petfecto_api.Migrations
{
    /// <inheritdoc />
    public partial class UserActiveProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Perfil_Personas_PersonaDni",
                table: "Perfil");

            migrationBuilder.AddColumn<bool>(
                name: "UserActivo",
                table: "Usuarios",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "PersonaDni",
                table: "Perfil",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Perfil_Personas_PersonaDni",
                table: "Perfil",
                column: "PersonaDni",
                principalTable: "Personas",
                principalColumn: "Dni",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Perfil_Personas_PersonaDni",
                table: "Perfil");

            migrationBuilder.DropColumn(
                name: "UserActivo",
                table: "Usuarios");

            migrationBuilder.AlterColumn<string>(
                name: "PersonaDni",
                table: "Perfil",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Perfil_Personas_PersonaDni",
                table: "Perfil",
                column: "PersonaDni",
                principalTable: "Personas",
                principalColumn: "Dni");
        }
    }
}
