using Microsoft.EntityFrameworkCore.Migrations;

namespace ChurchAppAPI.Migrations
{
    public partial class AddDonationTypeToDonation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DonationTypeID",
                table: "Donations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Donations_DonationTypeID",
                table: "Donations",
                column: "DonationTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_DonationTypes_DonationTypeID",
                table: "Donations",
                column: "DonationTypeID",
                principalTable: "DonationTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_DonationTypes_DonationTypeID",
                table: "Donations");

            migrationBuilder.DropIndex(
                name: "IX_Donations_DonationTypeID",
                table: "Donations");

            migrationBuilder.DropColumn(
                name: "DonationTypeID",
                table: "Donations");
        }
    }
}
