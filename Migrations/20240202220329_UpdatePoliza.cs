using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminPolizasAPI.Migrations
{
    public partial class UpdatePoliza : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CubreCerraduras",
                table: "Coberturas",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CubreCristalesLaterales",
                table: "Coberturas",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CubreDestruccionTotalAccidentes",
                table: "Coberturas",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CubreLunetasParabrisas",
                table: "Coberturas",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CubreResponsabilidadCivil",
                table: "Coberturas",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Coberturas",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CubreCerraduras",
                table: "Coberturas");

            migrationBuilder.DropColumn(
                name: "CubreCristalesLaterales",
                table: "Coberturas");

            migrationBuilder.DropColumn(
                name: "CubreDestruccionTotalAccidentes",
                table: "Coberturas");

            migrationBuilder.DropColumn(
                name: "CubreLunetasParabrisas",
                table: "Coberturas");

            migrationBuilder.DropColumn(
                name: "CubreResponsabilidadCivil",
                table: "Coberturas");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Coberturas");
        }
    }
}
