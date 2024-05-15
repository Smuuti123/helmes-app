using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelmesAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddBagWithParcels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Shipments_ShipmentNumber",
                table: "Shipments");

            migrationBuilder.DropIndex(
                name: "IX_Parcels_ParcelNumber",
                table: "Parcels");

            migrationBuilder.AlterColumn<string>(
                name: "BagNumber",
                table: "BagWithParcels",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BagNumber",
                table: "BagWithLetters",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_ShipmentNumber",
                table: "Shipments",
                column: "ShipmentNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_ParcelNumber",
                table: "Parcels",
                column: "ParcelNumber");

            migrationBuilder.CreateIndex(
                name: "IX_BagWithParcels_BagNumber",
                table: "BagWithParcels",
                column: "BagNumber");

            migrationBuilder.CreateIndex(
                name: "IX_BagWithLetters_BagNumber",
                table: "BagWithLetters",
                column: "BagNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Shipments_ShipmentNumber",
                table: "Shipments");

            migrationBuilder.DropIndex(
                name: "IX_Parcels_ParcelNumber",
                table: "Parcels");

            migrationBuilder.DropIndex(
                name: "IX_BagWithParcels_BagNumber",
                table: "BagWithParcels");

            migrationBuilder.DropIndex(
                name: "IX_BagWithLetters_BagNumber",
                table: "BagWithLetters");

            migrationBuilder.AlterColumn<string>(
                name: "BagNumber",
                table: "BagWithParcels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BagNumber",
                table: "BagWithLetters",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_ShipmentNumber",
                table: "Shipments",
                column: "ShipmentNumber",
                unique: true,
                filter: "[ShipmentNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_ParcelNumber",
                table: "Parcels",
                column: "ParcelNumber",
                unique: true,
                filter: "[ParcelNumber] IS NOT NULL");
        }
    }
}
