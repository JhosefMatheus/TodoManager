using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumnNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "user",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "user",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Login",
                table: "user",
                newName: "login");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "user",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "user",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "user",
                newName: "created_at");

            migrationBuilder.RenameIndex(
                name: "IX_user_Login",
                table: "user",
                newName: "IX_user_login");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "project",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "project",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "project",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "project",
                newName: "created_at");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "password",
                table: "user",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "user",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "login",
                table: "user",
                newName: "Login");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "user",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "user",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "user",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_user_login",
                table: "user",
                newName: "IX_user_Login");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "project",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "project",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "project",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "project",
                newName: "CreatedAt");
        }
    }
}
