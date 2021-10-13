using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Sales.Data.Migrations
{
    public partial class InitialSales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "OrderSequence",
                startValue: 1000L);

            migrationBuilder.CreateTable(
                name: "Vouchers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Percentage = table.Column<decimal>(nullable: true),
                    PriceDiscount = table.Column<decimal>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    TypeDiscount = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    UsedDate = table.Column<DateTime>(nullable: true),
                    ExpirationDate = table.Column<DateTime>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    Used = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vouchers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<int>(nullable: false, defaultValueSql: "NEXT VALUE FOR OrderSequence"),
                    CustomerId = table.Column<Guid>(nullable: false),
                    CouponId = table.Column<Guid>(nullable: true),
                    CouponUsed = table.Column<bool>(nullable: false),
                    Discount = table.Column<decimal>(nullable: false),
                    TotalPrice = table.Column<decimal>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    OrderStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Vouchers_CouponId",
                        column: x => x.CouponId,
                        principalTable: "Vouchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderLine",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    ProductName = table.Column<string>(type: "varchar(250)", maxLength: 100, nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    UnitPrice = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderLine_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_CouponId",
                table: "Order",
                column: "CouponId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLine_OrderId",
                table: "OrderLine",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderLine");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Vouchers");

            migrationBuilder.DropSequence(
                name: "OrderSequence");
        }
    }
}
