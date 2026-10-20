using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cuth_Alexandra_Lab2.Migrations
{
    /// <inheritdoc />
    public partial class PublishingDate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PublicationDate",
                table: "Book",
                newName: "PublishingDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PublishingDate",
                table: "Book",
                newName: "PublicationDate");
        }
    }
}
