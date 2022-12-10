using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EHealthCareApplication.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(table: "Accounts",
                            columns: new[] { "FirstName", "LastName", "Password", "Email", "DateOfBirth", "Phone", "Address", "IsAdmin" },
                            values: new object[]
                            {
                                            "Test",
                                            "User",
                                            "Password1",
                                            "testuser1@test.com",
                                            "2000-03-11",
                                            "987652",
                                            "India",
                                            "0",
                            },
                            schema: default);

            migrationBuilder.InsertData(table: "Accounts",
                            columns: new[] { "FirstName", "LastName", "Password", "Email", "DateOfBirth", "Phone", "Address", "IsAdmin" },
                            values: new object[]
                            {
                                            "Admin",
                                            "User",
                                            "Password1",
                                            "admin@test.com",
                                            "1993-09-21",
                                            "+1-123562",
                                            "Oman",
                                            "1"

                            },
                            schema: null);

            migrationBuilder.InsertData(table: "Products",
                            columns: new[] { "Name", "CompanyName", "Price", "Quantity", "Description", "ExpireDate" },
                            values: new object[]
                            {
                                            "Nutriosys Isabgol",
                                            "Company1",
                                            "525",
                                            "50",
                                            "Product for Nutriosys Isabgol",
                                            "12-12-2025",

                            },
                            schema: null);

            migrationBuilder.InsertData(table: "Products",
                           columns: new[] { "Name", "CompanyName", "Price", "Quantity", "Description", "ExpireDate" },
                           values: new object[]
                           {
                                            "Herbocalm tab",
                                            "Company2",
                                            "375",
                                            "50",
                                            "Product for Herbocalm Capsulesl",
                                            "2-1-2024",

                           },
                           schema: null);

            migrationBuilder.InsertData(table: "Products",
                           columns: new[] { "Name", "CompanyName", "Price", "Quantity", "Description", "ExpireDate" },
                           values: new object[]
                           {
                                            "Becosules tab",
                                            "Company3",
                                            "81",
                                            "40",
                                            "Product for Becosules Capsule 20",
                                            "1-1-2023",

                           },
                           schema: null);

            migrationBuilder.InsertData(table: "Products",
                           columns: new[] { "Name", "CompanyName", "Price", "Quantity", "Description", "ExpireDate" },
                           values: new object[]
                           {
                                            "Optineuron 3ml",
                                            "Company4",
                                            "534",
                                            "70",
                                            "Product for Optineuron Injection 3ml ",
                                            "3-6-2024",

                           },
                           schema: null);

            migrationBuilder.InsertData(table: "Products",
                           columns: new[] { "Name", "CompanyName", "Price", "Quantity", "Description", "ExpireDate" },
                           values: new object[]
                           {
                                            "Nutritional Powder",
                                            "Company5",
                                            "314",
                                            "10",
                                            "Product used for Nutritional values",
                                            "9-2-2025",

                           },
                           schema: null);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
