using Microsoft.EntityFrameworkCore.Migrations;

namespace ChurchAppAPI.Migrations
{
    public partial class AddNewFieldsInDonation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "DonationTypes",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCash",
                table: "Donations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCheck",
                table: "Donations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MemberId",
                table: "Donations",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCash",
                table: "Donations");

            migrationBuilder.DropColumn(
                name: "IsCheck",
                table: "Donations");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "Donations");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "DonationTypes",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20);
        }
    }
}
