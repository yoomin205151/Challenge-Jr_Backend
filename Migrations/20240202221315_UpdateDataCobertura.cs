using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminPolizasAPI.Migrations
{
    public partial class UpdateDataCobertura : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Coberturas");

            migrationBuilder.RenameColumn(
                name: "CubreResponsabilidadCivil",
                table: "Coberturas",
                newName: "ResponsabilidadCivil");

            migrationBuilder.RenameColumn(
                name: "CubreLunetasParabrisas",
                table: "Coberturas",
                newName: "LunetasParabrisas");

            migrationBuilder.RenameColumn(
                name: "CubreDestruccionTotalAccidentes",
                table: "Coberturas",
                newName: "DestruccionTotalAccidentes");

            migrationBuilder.RenameColumn(
                name: "CubreCristalesLaterales",
                table: "Coberturas",
                newName: "CristalesLaterales");

            migrationBuilder.RenameColumn(
                name: "CubreCerraduras",
                table: "Coberturas",
                newName: "Cerraduras");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ResponsabilidadCivil",
                table: "Coberturas",
                newName: "CubreResponsabilidadCivil");

            migrationBuilder.RenameColumn(
                name: "LunetasParabrisas",
                table: "Coberturas",
                newName: "CubreLunetasParabrisas");

            migrationBuilder.RenameColumn(
                name: "DestruccionTotalAccidentes",
                table: "Coberturas",
                newName: "CubreDestruccionTotalAccidentes");

            migrationBuilder.RenameColumn(
                name: "CristalesLaterales",
                table: "Coberturas",
                newName: "CubreCristalesLaterales");

            migrationBuilder.RenameColumn(
                name: "Cerraduras",
                table: "Coberturas",
                newName: "CubreCerraduras");

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Coberturas",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");
        }
    }
}
