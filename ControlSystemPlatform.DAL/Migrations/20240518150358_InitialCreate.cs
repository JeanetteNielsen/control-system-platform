using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlSystemPlatform.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SKU = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(265)", maxLength: 265, nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(8,3)", precision: 8, scale: 3, nullable: false),
                    Length = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: true),
                    Width = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: true),
                    Height = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: true),
                    SpecialHandlingInstructions = table.Column<string>(type: "nvarchar(265)", maxLength: 265, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransportOrder",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequesterOrderReference = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SourceLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DestinationLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RequiredCompletionTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssignedResource = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    HandlingStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    HandlingInstructions = table.Column<string>(type: "nvarchar(265)", maxLength: 265, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportOrder", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ErrorHandlingProtocol",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Issue = table.Column<string>(type: "nvarchar(265)", maxLength: 265, nullable: true),
                    ResolutionSteps = table.Column<string>(type: "nvarchar(265)", maxLength: 265, nullable: true),
                    TransportOrderEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorHandlingProtocol", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ErrorHandlingProtocol_TransportOrder_TransportOrderEntityId",
                        column: x => x.TransportOrderEntityId,
                        principalTable: "TransportOrder",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransportOrderEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventDescription = table.Column<string>(type: "nvarchar(265)", maxLength: 265, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_TransportOrder_TransportOrderEntityId",
                        column: x => x.TransportOrderEntityId,
                        principalTable: "TransportOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItemEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransportOrderEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItemEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItemEntity_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItemEntity_TransportOrder_TransportOrderEntityId",
                        column: x => x.TransportOrderEntityId,
                        principalTable: "TransportOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RouteInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransportOrderEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Waypoints = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlternatePath = table.Column<string>(type: "nvarchar(265)", maxLength: 265, nullable: true),
                    SpecialNavigationInstructions = table.Column<string>(type: "nvarchar(265)", maxLength: 265, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RouteInfo_TransportOrder_TransportOrderEntityId",
                        column: x => x.TransportOrderEntityId,
                        principalTable: "TransportOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ErrorHandlingProtocol_TransportOrderEntityId",
                table: "ErrorHandlingProtocol",
                column: "TransportOrderEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_TransportOrderEntityId",
                table: "Events",
                column: "TransportOrderEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemEntity_ItemId",
                table: "OrderItemEntity",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemEntity_TransportOrderEntityId",
                table: "OrderItemEntity",
                column: "TransportOrderEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteInfo_TransportOrderEntityId",
                table: "RouteInfo",
                column: "TransportOrderEntityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransportOrder_RequesterOrderReference",
                table: "TransportOrder",
                column: "RequesterOrderReference",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ErrorHandlingProtocol");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "OrderItemEntity");

            migrationBuilder.DropTable(
                name: "RouteInfo");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "TransportOrder");
        }
    }
}
