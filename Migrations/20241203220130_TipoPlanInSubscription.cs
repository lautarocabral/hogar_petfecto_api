using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hogar_petfecto_api.Migrations
{
    /// <inheritdoc />
    public partial class TipoPlanInSubscription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TipoPlan",
                table: "Suscripciones",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoPlan",
                table: "Suscripciones");
        }
    }
}
