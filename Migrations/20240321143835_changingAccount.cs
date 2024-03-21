using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pix.Migrations
{
    /// <inheritdoc />
    public partial class changingAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Keys_AccountId",
                table: "Keys",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Keys_Accounts_AccountId",
                table: "Keys",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Keys_Accounts_AccountId",
                table: "Keys");

            migrationBuilder.DropIndex(
                name: "IX_Keys_AccountId",
                table: "Keys");
        }
    }
}
