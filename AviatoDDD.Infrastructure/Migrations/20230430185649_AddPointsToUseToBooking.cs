using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AviatoDDD.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddPointsToUseToBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PointsToUse",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PointsToUse",
                table: "Bookings");
        }
    }
}
