using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class DefectStatusInHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Defects_DefectStatuses_StatusId",
                table: "Defects");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Defects",
                newName: "DefectStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Defects_StatusId",
                table: "Defects",
                newName: "IX_Defects_DefectStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Defects_DefectStatuses_DefectStatusId",
                table: "Defects",
                column: "DefectStatusId",
                principalTable: "DefectStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Defects_DefectStatuses_DefectStatusId",
                table: "Defects");

            migrationBuilder.RenameColumn(
                name: "DefectStatusId",
                table: "Defects",
                newName: "StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Defects_DefectStatusId",
                table: "Defects",
                newName: "IX_Defects_StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Defects_DefectStatuses_StatusId",
                table: "Defects",
                column: "StatusId",
                principalTable: "DefectStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
