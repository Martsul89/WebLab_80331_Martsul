using Microsoft.EntityFrameworkCore.Migrations;

namespace WebLab.DAL.Migrations
{
    public partial class EntitiesAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FlowerGroups",
                columns: table => new
                {
                    FlowerGroupId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowerGroups", x => x.FlowerGroupId);
                });

            migrationBuilder.CreateTable(
                name: "Flowers",
                columns: table => new
                {
                    FlowerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlowerName = table.Column<string>(nullable: true),
                    FlowerDescription = table.Column<string>(nullable: true),
                    FlowerPrice = table.Column<int>(nullable: false),
                    FlowerImage = table.Column<string>(nullable: true),
                    FlowerGroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flowers", x => x.FlowerId);
                    table.ForeignKey(
                        name: "FK_Flowers_FlowerGroups_FlowerGroupId",
                        column: x => x.FlowerGroupId,
                        principalTable: "FlowerGroups",
                        principalColumn: "FlowerGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flowers_FlowerGroupId",
                table: "Flowers",
                column: "FlowerGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flowers");

            migrationBuilder.DropTable(
                name: "FlowerGroups");
        }
    }
}
