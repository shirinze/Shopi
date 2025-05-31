using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shopi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedproduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "GoldSequence",
                schema: "Sho");

            migrationBuilder.CreateSequence(
                name: "PhoneSequence",
                schema: "Sho");

            migrationBuilder.CreateSequence(
                name: "ShoesSequence",
                schema: "Sho");

            migrationBuilder.CreateTable(
                name: "Gold",
                schema: "Sho",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [Sho].[GoldSequence]"),
                    Karat = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gold", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Phone",
                schema: "Sho",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [Sho].[PhoneSequence]"),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phone", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shoes",
                schema: "Sho",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [Sho].[ShoesSequence]"),
                    Size = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shoes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gold",
                schema: "Sho");

            migrationBuilder.DropTable(
                name: "Phone",
                schema: "Sho");

            migrationBuilder.DropTable(
                name: "Shoes",
                schema: "Sho");

            migrationBuilder.DropSequence(
                name: "GoldSequence",
                schema: "Sho");

            migrationBuilder.DropSequence(
                name: "PhoneSequence",
                schema: "Sho");

            migrationBuilder.DropSequence(
                name: "ShoesSequence",
                schema: "Sho");
        }
    }
}
