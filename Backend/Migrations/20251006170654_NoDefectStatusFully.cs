using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class NoDefectStatusFully : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Defects_DefectStatuses_DefectStatusId",
                table: "Defects");

            migrationBuilder.DropIndex(
                name: "IX_Defects_DefectStatusId",
                table: "Defects");

            migrationBuilder.DropColumn(
                name: "DefectStatusId",
                table: "Defects");

            migrationBuilder.AddColumn<int>(
                name: "DefectStatusEntityId",
                table: "Defects",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Defects_DefectStatusEntityId",
                table: "Defects",
                column: "DefectStatusEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Defects_DefectStatuses_DefectStatusEntityId",
                table: "Defects",
                column: "DefectStatusEntityId",
                principalTable: "DefectStatuses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Defects_DefectStatuses_DefectStatusEntityId",
                table: "Defects");

            migrationBuilder.DropIndex(
                name: "IX_Defects_DefectStatusEntityId",
                table: "Defects");

            migrationBuilder.DropColumn(
                name: "DefectStatusEntityId",
                table: "Defects");

            migrationBuilder.AddColumn<int>(
                name: "DefectStatusId",
                table: "Defects",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Defects_DefectStatusId",
                table: "Defects",
                column: "DefectStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Defects_DefectStatuses_DefectStatusId",
                table: "Defects",
                column: "DefectStatusId",
                principalTable: "DefectStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
