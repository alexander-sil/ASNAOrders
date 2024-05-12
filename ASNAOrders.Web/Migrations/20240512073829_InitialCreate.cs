using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASNAOrders.Web.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Barcodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Values = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    WeightEncoding = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Barcodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryAddrs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Full = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryAddrs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Discriminator = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ClientName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ClientArrivementDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemDescs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    General = table.Column<string>(type: "nvarchar(max)", maxLength: 4096, nullable: true),
                    Composition = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NutritionalValue = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Purpose = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    StorageRequirements = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ExpiresIn = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    VendorCountry = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    PackageInfo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VendorName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemDescs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemMeasures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<int>(type: "int", nullable: false),
                    Quantum = table.Column<float>(type: "real", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemMeasures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NativeStocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UploadRecordedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlaceId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Composition = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    ItemName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ItemId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Qtty = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    ItemDesc = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Barcode = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NativeStocks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Discriminator = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PaymentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemsCost = table.Column<double>(type: "float", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false),
                    Change = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "YandexEatsVolumes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YandexEatsVolumes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<int>(type: "int", nullable: true),
                    Hash = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryImages_Categories_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Platform = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    EatsId = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    RestaurantId = table.Column<string>(type: "nvarchar(48)", maxLength: 48, nullable: true),
                    DeliveryInfoId = table.Column<int>(type: "int", nullable: false),
                    PaymentInfoId = table.Column<int>(type: "int", nullable: false),
                    Persons = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_DeliveryInfos_DeliveryInfoId",
                        column: x => x.DeliveryInfoId,
                        principalTable: "DeliveryInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_PaymentInfos_PaymentInfoId",
                        column: x => x.PaymentInfoId,
                        principalTable: "PaymentInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "YENomenclatureItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorCode = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    CategoryId = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DescriptionId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    OldPrice = table.Column<float>(type: "real", nullable: true),
                    Vat = table.Column<int>(type: "int", nullable: false),
                    BarcodeId = table.Column<int>(type: "int", nullable: false),
                    MeasureId = table.Column<int>(type: "int", nullable: false),
                    VolumeId = table.Column<int>(type: "int", nullable: true),
                    IsCatchWeight = table.Column<bool>(type: "bit", nullable: false),
                    ExciseValue = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    Labels = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YENomenclatureItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_YENomenclatureItems_Barcodes_BarcodeId",
                        column: x => x.BarcodeId,
                        principalTable: "Barcodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_YENomenclatureItems_ItemDescs_DescriptionId",
                        column: x => x.DescriptionId,
                        principalTable: "ItemDescs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_YENomenclatureItems_ItemMeasures_MeasureId",
                        column: x => x.MeasureId,
                        principalTable: "ItemMeasures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_YENomenclatureItems_YandexEatsVolumes_VolumeId",
                        column: x => x.VolumeId,
                        principalTable: "YandexEatsVolumes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Quantity = table.Column<float>(type: "real", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderPromos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPromos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderPromos_Orders_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ItemImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<int>(type: "int", nullable: true),
                    Hash = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemImages_YENomenclatureItems_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "YENomenclatureItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ItemPromos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPromos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemPromos_Items_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Items",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Modifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Modifications_Items_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Items",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryImages_OwnerId",
                table: "CategoryImages",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemImages_OwnerId",
                table: "ItemImages",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPromos_OwnerId",
                table: "ItemPromos",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_OrderId",
                table: "Items",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Modifications_OwnerId",
                table: "Modifications",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPromos_OwnerId",
                table: "OrderPromos",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliveryInfoId",
                table: "Orders",
                column: "DeliveryInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentInfoId",
                table: "Orders",
                column: "PaymentInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_YENomenclatureItems_BarcodeId",
                table: "YENomenclatureItems",
                column: "BarcodeId");

            migrationBuilder.CreateIndex(
                name: "IX_YENomenclatureItems_DescriptionId",
                table: "YENomenclatureItems",
                column: "DescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_YENomenclatureItems_MeasureId",
                table: "YENomenclatureItems",
                column: "MeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_YENomenclatureItems_VolumeId",
                table: "YENomenclatureItems",
                column: "VolumeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryImages");

            migrationBuilder.DropTable(
                name: "DeliveryAddrs");

            migrationBuilder.DropTable(
                name: "ItemImages");

            migrationBuilder.DropTable(
                name: "ItemPromos");

            migrationBuilder.DropTable(
                name: "Modifications");

            migrationBuilder.DropTable(
                name: "NativeStocks");

            migrationBuilder.DropTable(
                name: "OrderPromos");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "YENomenclatureItems");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Barcodes");

            migrationBuilder.DropTable(
                name: "ItemDescs");

            migrationBuilder.DropTable(
                name: "ItemMeasures");

            migrationBuilder.DropTable(
                name: "YandexEatsVolumes");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "DeliveryInfos");

            migrationBuilder.DropTable(
                name: "PaymentInfos");
        }
    }
}
