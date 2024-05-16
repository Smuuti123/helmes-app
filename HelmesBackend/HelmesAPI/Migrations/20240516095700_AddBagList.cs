using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelmesAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddBagList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parcels_Bag_BagWithParcelsId",
                table: "Parcels");

            migrationBuilder.RenameColumn(
                name: "BagType",
                table: "Bag",
                newName: "Discriminator");

            migrationBuilder.AddForeignKey(
                name: "FK_Parcels_Bag_BagWithParcelsId",
                table: "Parcels",
                column: "BagWithParcelsId",
                principalTable: "Bag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parcels_Bag_BagWithParcelsId",
                table: "Parcels");

            migrationBuilder.RenameColumn(
                name: "Discriminator",
                table: "Bag",
                newName: "BagType");

            migrationBuilder.AddForeignKey(
                name: "FK_Parcels_Bag_BagWithParcelsId",
                table: "Parcels",
                column: "BagWithParcelsId",
                principalTable: "Bag",
                principalColumn: "Id");
        }
    }
}
