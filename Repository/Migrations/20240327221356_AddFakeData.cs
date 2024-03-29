using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddFakeData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Women");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Men");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Watches");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Babies");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Amount", "CreateAt" },
                values: new object[] { 399.89999999999998, new DateTimeOffset(new DateTime(2024, 3, 27, 22, 13, 55, 469, DateTimeKind.Unspecified).AddTicks(755), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Amount", "CreateAt" },
                values: new object[] { 119.98, new DateTimeOffset(new DateTime(2024, 3, 26, 22, 13, 55, 469, DateTimeKind.Unspecified).AddTicks(764), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name", "Price", "Quantity" },
                values: new object[] { "This elegant women's dress is perfect for any special occasion. It features delicate lace detailing and a flattering silhouette. Made from high-quality materials for comfort and style. With its timeless design and impeccable craftsmanship, this dress will make you stand out from the crowd. Whether you're attending a wedding, cocktail party, or formal dinner, this dress is sure to turn heads. Pair it with heels and statement jewelry for a glamorous look that will make you feel like a million dollars.\r\n\r\nElevate your wardrobe with this stunning lace dress. The intricate lace overlay adds a touch of sophistication, while the classic silhouette ensures a flattering fit. Whether you're dancing the night away at a gala or enjoying a romantic dinner, this dress is the perfect choice for making a statement. Crafted from luxurious fabric with a silky lining, it offers both style and comfort. Add it to your collection and be prepared to dazzle on any occasion.\r\n\r\nMake a lasting impression with this timeless lace dress. The elegant design and exquisite detailing make it a standout piece in any wardrobe. Whether you're celebrating a special milestone or attending a formal event, this dress will ensure you look and feel your best. With its versatile style, you can easily dress it up or down to suit any occasion. Complete your look with heels and accessories for a polished ensemble that exudes confidence and grace.", "Elegant Lace Dress", 59.990000000000002, 50 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Image", "Name", "Price", "Quantity" },
                values: new object[] { "This stylish men's shirt is a wardrobe essential. With its slim fit design and classic button-down collar, it's perfect for both casual and formal occasions. Made from premium cotton for all-day comfort. Whether you're heading to the office, going out for drinks with friends, or enjoying a weekend brunch, this shirt will keep you looking sharp and feeling confident. Pair it with chinos or jeans for a polished yet relaxed look that effortlessly transitions from day to night.\r\n\r\nElevate your style with this modern slim fit shirt. The tailored silhouette and sleek design create a polished look that's perfect for any occasion. Crafted from premium cotton fabric, it offers a comfortable fit and exceptional durability. Whether you're dressing for work or play, this shirt is sure to impress. Add it to your wardrobe and enjoy effortless style wherever you go.\r\n\r\nStay on-trend with this versatile button-down shirt. The slim fit silhouette and timeless design make it a must-have for every man's closet. Whether you're dressing for a business meeting or a night out on the town, this shirt will ensure you look your best. Made from high-quality materials, it offers both style and comfort. Pair it with trousers for a sophisticated look or wear it with jeans for a more casual vibe.", "Image 2", "Slim Fit Button-down Shirt", 39.990000000000002, 40 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Category 1");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Category 2");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Category 3");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Category 4");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Amount", "CreateAt" },
                values: new object[] { 150.0, new DateTimeOffset(new DateTime(2024, 3, 27, 10, 3, 56, 530, DateTimeKind.Unspecified).AddTicks(3064), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Amount", "CreateAt" },
                values: new object[] { 100.0, new DateTimeOffset(new DateTime(2024, 3, 26, 10, 3, 56, 530, DateTimeKind.Unspecified).AddTicks(3067), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name", "Price", "Quantity" },
                values: new object[] { "Description 1", "Product 1", 10.99, 100 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Image", "Name", "Price", "Quantity" },
                values: new object[] { "Description 2", "Image 1", "Product 2", 20.5, 50 });
        }
    }
}
