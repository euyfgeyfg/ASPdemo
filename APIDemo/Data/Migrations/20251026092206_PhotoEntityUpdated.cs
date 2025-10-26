using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIDemo.Data.Migrations
{
    /// <inheritdoc />
    public partial class PhotoEntityUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Gendet",
                table: "Members",
                newName: "Gender");

            migrationBuilder.RenameColumn(
                name: "CreateTime",
                table: "Members",
                newName: "Created");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Members",
                newName: "Gendet");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Members",
                newName: "CreateTime");
        }
    }
}
