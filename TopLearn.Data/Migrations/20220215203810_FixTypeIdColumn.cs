using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TopLearn.Data.Migrations
{
    public partial class FixTypeIdColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallet_WalletType_WalletTypeTypeId",
                table: "Wallet");

            migrationBuilder.DropIndex(
                name: "IX_Wallet_WalletTypeTypeId",
                table: "Wallet");

            migrationBuilder.DropColumn(
                name: "WalletTypeTypeId",
                table: "Wallet");

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_TypeId",
                table: "Wallet",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallet_WalletType_TypeId",
                table: "Wallet",
                column: "TypeId",
                principalTable: "WalletType",
                principalColumn: "TypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallet_WalletType_TypeId",
                table: "Wallet");

            migrationBuilder.DropIndex(
                name: "IX_Wallet_TypeId",
                table: "Wallet");

            migrationBuilder.AddColumn<int>(
                name: "WalletTypeTypeId",
                table: "Wallet",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_WalletTypeTypeId",
                table: "Wallet",
                column: "WalletTypeTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallet_WalletType_WalletTypeTypeId",
                table: "Wallet",
                column: "WalletTypeTypeId",
                principalTable: "WalletType",
                principalColumn: "TypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
