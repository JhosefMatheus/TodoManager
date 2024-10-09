using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class CreateTaskTypeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "task_type_id",
                table: "task",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "task_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_task_type", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_task_task_type_id",
                table: "task",
                column: "task_type_id");

            migrationBuilder.AddForeignKey(
                name: "FK_task_task_type_task_type_id",
                table: "task",
                column: "task_type_id",
                principalTable: "task_type",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_task_task_type_task_type_id",
                table: "task");

            migrationBuilder.DropTable(
                name: "task_type");

            migrationBuilder.DropIndex(
                name: "IX_task_task_type_id",
                table: "task");

            migrationBuilder.DropColumn(
                name: "task_type_id",
                table: "task");
        }
    }
}
