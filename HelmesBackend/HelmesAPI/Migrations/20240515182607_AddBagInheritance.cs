using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelmesAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddBagInheritance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parcels_BagWithParcels_BagWithParcelsId",
                table: "Parcels");

            migrationBuilder.DropForeignKey(
                name: "FK_Parcels_Shipments_ShipmentId",
                table: "Parcels");

            migrationBuilder.DropTable(
                name: "BagWithLetters");

            migrationBuilder.DropIndex(
                name: "IX_Shipments_ShipmentNumber",
                table: "Shipments");

            migrationBuilder.DropIndex(
                name: "IX_Parcels_ParcelNumber",
                table: "Parcels");

            migrationBuilder.DropIndex(
                name: "IX_Parcels_ShipmentId",
                table: "Parcels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BagWithParcels",
                table: "BagWithParcels");

            migrationBuilder.DropIndex(
                name: "IX_BagWithParcels_BagNumber",
                table: "BagWithParcels");

            migrationBuilder.DropColumn(
                name: "ShipmentId",
                table: "Parcels");

            migrationBuilder.RenameTable(
                name: "BagWithParcels",
                newName: "Bag");

            migrationBuilder.RenameColumn(
                name: "KnownAirport",
                table: "Shipments",
                newName: "Status");

            migrationBuilder.AlterColumn<string>(
                name: "ShipmentNumber",
                table: "Shipments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FlightDate",
                table: "Shipments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Airport",
                table: "Shipments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FlightNumber",
                table: "Shipments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BagType",
                table: "Bag",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CountOfLetters",
                table: "Bag",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Bag",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShipmentId",
                table: "Bag",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Weight",
                table: "Bag",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bag",
                table: "Bag",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_ShipmentNumber",
                table: "Shipments",
                column: "ShipmentNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_ParcelNumber",
                table: "Parcels",
                column: "ParcelNumber",
                unique: true,
                filter: "[ParcelNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Bag_BagNumber",
                table: "Bag",
                column: "BagNumber",
                unique: true,
                filter: "[BagNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Bag_ShipmentId",
                table: "Bag",
                column: "ShipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bag_Shipments_ShipmentId",
                table: "Bag",
                column: "ShipmentId",
                principalTable: "Shipments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Parcels_Bag_BagWithParcelsId",
                table: "Parcels",
                column: "BagWithParcelsId",
                principalTable: "Bag",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bag_Shipments_ShipmentId",
                table: "Bag");

            migrationBuilder.DropForeignKey(
                name: "FK_Parcels_Bag_BagWithParcelsId",
                table: "Parcels");

            migrationBuilder.DropIndex(
                name: "IX_Shipments_ShipmentNumber",
                table: "Shipments");

            migrationBuilder.DropIndex(
                name: "IX_Parcels_ParcelNumber",
                table: "Parcels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bag",
                table: "Bag");

            migrationBuilder.DropIndex(
                name: "IX_Bag_BagNumber",
                table: "Bag");

            migrationBuilder.DropIndex(
                name: "IX_Bag_ShipmentId",
                table: "Bag");

            migrationBuilder.DropColumn(
                name: "Airport",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "FlightNumber",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "BagType",
                table: "Bag");

            migrationBuilder.DropColumn(
                name: "CountOfLetters",
                table: "Bag");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Bag");

            migrationBuilder.DropColumn(
                name: "ShipmentId",
                table: "Bag");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Bag");

            migrationBuilder.RenameTable(
                name: "Bag",
                newName: "BagWithParcels");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Shipments",
                newName: "KnownAirport");

            migrationBuilder.AlterColumn<string>(
                name: "ShipmentNumber",
                table: "Shipments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "FlightDate",
                table: "Shipments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "ShipmentId",
                table: "Parcels",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BagWithParcels",
                table: "BagWithParcels",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "BagWithLetters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BagNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CountOfLetters = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BagWithLetters", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_ShipmentNumber",
                table: "Shipments",
                column: "ShipmentNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_ParcelNumber",
                table: "Parcels",
                column: "ParcelNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_ShipmentId",
                table: "Parcels",
                column: "ShipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_BagWithParcels_BagNumber",
                table: "BagWithParcels",
                column: "BagNumber");

            migrationBuilder.CreateIndex(
                name: "IX_BagWithLetters_BagNumber",
                table: "BagWithLetters",
                column: "BagNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Parcels_BagWithParcels_BagWithParcelsId",
                table: "Parcels",
                column: "BagWithParcelsId",
                principalTable: "BagWithParcels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Parcels_Shipments_ShipmentId",
                table: "Parcels",
                column: "ShipmentId",
                principalTable: "Shipments",
                principalColumn: "Id");
        }
    }
}
