using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiblioTechData.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTablesFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Type_Permission_PermissionId",
                table: "Type");

            migrationBuilder.DropIndex(
                name: "IX_Type_PermissionId",
                table: "Type");

            migrationBuilder.DropColumn(
                name: "PermissionId",
                table: "Type");

            migrationBuilder.AddColumn<long>(
                name: "TypeId",
                table: "Permission",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Permission_TypeId",
                table: "Permission",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Permission_Type_TypeId",
                table: "Permission",
                column: "TypeId",
                principalTable: "Type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permission_Type_TypeId",
                table: "Permission");

            migrationBuilder.DropIndex(
                name: "IX_Permission_TypeId",
                table: "Permission");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Permission");

            migrationBuilder.AddColumn<long>(
                name: "PermissionId",
                table: "Type",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Type_PermissionId",
                table: "Type",
                column: "PermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Type_Permission_PermissionId",
                table: "Type",
                column: "PermissionId",
                principalTable: "Permission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
