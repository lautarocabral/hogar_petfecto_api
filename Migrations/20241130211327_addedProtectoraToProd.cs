using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hogar_petfecto_api.Migrations
{
    /// <inheritdoc />
    public partial class addedProtectoraToProd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Perfil_ProtectoraId",
                table: "Productos");

            migrationBuilder.AlterColumn<int>(
                name: "ProtectoraId",
                table: "Productos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Perfil_ProtectoraId",
                table: "Productos",
                column: "ProtectoraId",
                principalTable: "Perfil",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Perfil_ProtectoraId",
                table: "Productos");

            migrationBuilder.AlterColumn<int>(
                name: "ProtectoraId",
                table: "Productos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Perfil_ProtectoraId",
                table: "Productos",
                column: "ProtectoraId",
                principalTable: "Perfil",
                principalColumn: "Id");
        }
    }
}
