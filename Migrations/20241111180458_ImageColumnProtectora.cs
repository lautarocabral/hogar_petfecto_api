using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hogar_petfecto_api.Migrations
{
    /// <inheritdoc />
    public partial class ImageColumnProtectora : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaNacimiento",
                table: "Perfil");

            migrationBuilder.AddColumn<string>(
                name: "Imagen",
                table: "Mascotas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagen",
                table: "Mascotas");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaNacimiento",
                table: "Perfil",
                type: "datetime2",
                nullable: true);
        }
    }
}
