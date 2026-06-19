using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Midterm_BCS240037.Migrations
{
    /// <inheritdoc />
    public partial class Init_DB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DishCategories_BCS240037",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DishCategories_BCS240037", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dishes_BCS240037",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PreparationTime = table.Column<int>(type: "int", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DishCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dishes_BCS240037", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dishes_BCS240037_DishCategories_BCS240037_DishCategoryId",
                        column: x => x.DishCategoryId,
                        principalTable: "DishCategories_BCS240037",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DishImages_BCS240037",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsThumbnail = table.Column<bool>(type: "bit", nullable: false),
                    DishId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DishImages_BCS240037", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DishImages_BCS240037_Dishes_BCS240037_DishId",
                        column: x => x.DishId,
                        principalTable: "Dishes_BCS240037",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dishes_BCS240037_DishCategoryId",
                table: "Dishes_BCS240037",
                column: "DishCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Dishes_BCS240037_Name_DishCategoryId",
                table: "Dishes_BCS240037",
                columns: new[] { "Name", "DishCategoryId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DishImages_BCS240037_DishId",
                table: "DishImages_BCS240037",
                column: "DishId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DishImages_BCS240037");

            migrationBuilder.DropTable(
                name: "Dishes_BCS240037");

            migrationBuilder.DropTable(
                name: "DishCategories_BCS240037");
        }
    }
}
