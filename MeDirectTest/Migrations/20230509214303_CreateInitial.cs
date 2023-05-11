using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeDirectTest.Migrations
{
    /// <inheritdoc />
    public partial class CreateInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransactionContext",
                columns: table => new
                {
                    TransactionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TrClientId = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TrFirstName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TrLastName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TrFromCurrency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    TrFromAmount = table.Column<double>(type: "float", maxLength: 300, nullable: false),
                    TrToCurrency = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TrRate = table.Column<double>(type: "float", maxLength: 300, nullable: false),
                    TrResult = table.Column<double>(type: "float", maxLength: 300, nullable: false),
                    TrRateTimestamp = table.Column<DateTime>(type: "datetime2", maxLength: 300, nullable: false),
                    TransactionTimestamp = table.Column<DateTime>(type: "datetime2", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionContext", x => x.TransactionId);
                });

            migrationBuilder.CreateTable(
                name: "UserContext",
                columns: table => new
                {
                    ClientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserContext", x => x.ClientId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionContext");

            migrationBuilder.DropTable(
                name: "UserContext");
        }
    }
}
