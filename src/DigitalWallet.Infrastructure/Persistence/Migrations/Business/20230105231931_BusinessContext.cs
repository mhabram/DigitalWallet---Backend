using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalWallet.Infrastructure.Persistence.Migrations.Business
{
    public partial class BusinessContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Business");

            migrationBuilder.EnsureSchema(
                name: "Identity");

            migrationBuilder.CreateTable(
                name: "InvestmentWallets",
                schema: "Business",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Balance = table.Column<double>(type: "double precision", precision: 2, nullable: false),
                    Currency = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    WalletId = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestmentWallets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                schema: "Business",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    CountryCode = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UserName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    CountryCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Investments",
                schema: "Business",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<double>(type: "double precision", precision: 2, nullable: false),
                    Currency = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Category = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    DateTimeStart = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateTimeEnd = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    InvestmentWalletId = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Investments_InvestmentWallets_InvestmentWalletId",
                        column: x => x.InvestmentWalletId,
                        principalSchema: "Business",
                        principalTable: "InvestmentWallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvestmentWalletHistories",
                schema: "Business",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<double>(type: "double precision", precision: 2, nullable: false),
                    Currency = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Purchase = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    InvestmentWalletId = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestmentWalletHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvestmentWalletHistories_InvestmentWallets_InvestmentWalle~",
                        column: x => x.InvestmentWalletId,
                        principalSchema: "Business",
                        principalTable: "InvestmentWallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wallets",
                schema: "Business",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Balance = table.Column<double>(type: "double precision", precision: 2, nullable: false),
                    Currency = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wallets_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WalletHistories",
                schema: "Business",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<double>(type: "double precision", precision: 2, nullable: false),
                    Currency = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Deposit = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    WalletId = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WalletHistories_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalSchema: "Business",
                        principalTable: "Wallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Investments_InvestmentWalletId",
                schema: "Business",
                table: "Investments",
                column: "InvestmentWalletId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentWalletHistories_InvestmentWalletId",
                schema: "Business",
                table: "InvestmentWalletHistories",
                column: "InvestmentWalletId");

            migrationBuilder.CreateIndex(
                name: "IX_WalletHistories_WalletId",
                schema: "Business",
                table: "WalletHistories",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_UserId",
                schema: "Business",
                table: "Wallets",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Investments",
                schema: "Business");

            migrationBuilder.DropTable(
                name: "InvestmentWalletHistories",
                schema: "Business");

            migrationBuilder.DropTable(
                name: "Persons",
                schema: "Business");

            migrationBuilder.DropTable(
                name: "WalletHistories",
                schema: "Business");

            migrationBuilder.DropTable(
                name: "InvestmentWallets",
                schema: "Business");

            migrationBuilder.DropTable(
                name: "Wallets",
                schema: "Business");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "Identity");
        }
    }
}
