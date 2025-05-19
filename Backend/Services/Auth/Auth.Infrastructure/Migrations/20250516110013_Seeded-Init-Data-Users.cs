using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Auth.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeededInitDataUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("24da146e-0354-43bd-9e2a-dac1bf5c04fe"), "36fda167-d956-4215-8ca6-8845d07fbc0c", "USER", "USER" },
                    { new Guid("812c4430-2c72-49e7-8569-7abb5b2adf76"), "29720bdb-64f1-4aaf-b019-a938eaf9346f", "TEACHER", "TEACHER" },
                    { new Guid("8bc0903b-5f78-4723-a492-a2a825ae44f7"), "67a174e8-a980-41f1-a60b-bd1be92cdbaf", "STUDENT", "STUDENT" },
                    { new Guid("f8a8d71c-1d62-4697-a4cb-989df1d76866"), "25de8d7f-c72b-410d-a68a-9d8b9d81745c", "ADMIN", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Active", "AuthDate", "ConcurrencyStamp", "CreatedBy", "CreatedDate", "Email", "EmailConfirmed", "FirstName", "Hash", "Language", "LastModifiedBy", "LastModifiedDate", "LastName", "LockoutEnabled", "LockoutEnd", "MainRole", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhotoUrl", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TelegramId", "TelegramUserName", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("3c32cbde-34f1-497a-a332-0d98187ce761"), 0, false, null, "f70eb5de-bfc9-4e8c-84f1-e445f76a7aa8", null, null, null, false, "UserFirstName", null, 0, null, null, "UserLastName", false, null, "USER", null, "USER", "AQAAAAIAAYagAAAAEPUsoWcx4HATG2SmYWxtMfZxtyGWoXxNnTGSHTnd/u7RSbtESRDI72xfv6T54KSK6w==", null, false, null, null, null, null, null, null, false, "user" },
                    { new Guid("47c65afa-8621-4206-ad80-d065774bc33a"), 0, false, null, "f97e49c4-0e41-4de6-a742-917c6437fd9c", null, null, null, false, "AdminFirstName", null, 0, null, null, "AdminLastName", false, null, "ADMIN", null, "ADMIN", "AQAAAAIAAYagAAAAEN0gV+qjdHe1XcBjbzjExdjLzpnihvr+bgjXwSGZmQOSHzIYPi/0CkleCeIQ0RqA8Q==", null, false, null, null, null, null, null, null, false, "admin" },
                    { new Guid("4bdf852e-b8f5-48d4-9213-7b142224f6b0"), 0, false, null, "29549ecc-2fcb-4635-b989-d74d4cc35d2a", null, null, null, false, "StudentFirstName", null, 0, null, null, "StudentLastName", false, null, "STUDENT", null, "STUDENT", "AQAAAAIAAYagAAAAEHLZHQhIqoJGIltwkM/hwrNtcnPfEklcLmaJG753nk2zq3nO5CNIXvhemige6Y8bSQ==", null, false, null, null, null, null, null, null, false, "student" },
                    { new Guid("cdc70f49-6ec5-4cc3-bc6b-ec0d21652675"), 0, false, null, "d9cda0b2-0dbe-45e6-8442-51cbe34bd257", null, null, null, false, "TeacherFirstName", null, 0, null, null, "TeacherLastName", false, null, "TEACHER", null, "TEACHER", "AQAAAAIAAYagAAAAEJ+0j1xvgdDj667aDfX4KvVwUhHfhx8FnAYov/f08ZWZT1czD3l+XXMuTmQNUtQzcg==", null, false, null, null, null, null, null, null, false, "teacher" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("24da146e-0354-43bd-9e2a-dac1bf5c04fe"), new Guid("3c32cbde-34f1-497a-a332-0d98187ce761") },
                    { new Guid("f8a8d71c-1d62-4697-a4cb-989df1d76866"), new Guid("47c65afa-8621-4206-ad80-d065774bc33a") },
                    { new Guid("8bc0903b-5f78-4723-a492-a2a825ae44f7"), new Guid("4bdf852e-b8f5-48d4-9213-7b142224f6b0") },
                    { new Guid("812c4430-2c72-49e7-8569-7abb5b2adf76"), new Guid("cdc70f49-6ec5-4cc3-bc6b-ec0d21652675") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("24da146e-0354-43bd-9e2a-dac1bf5c04fe"), new Guid("3c32cbde-34f1-497a-a332-0d98187ce761") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("f8a8d71c-1d62-4697-a4cb-989df1d76866"), new Guid("47c65afa-8621-4206-ad80-d065774bc33a") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("8bc0903b-5f78-4723-a492-a2a825ae44f7"), new Guid("4bdf852e-b8f5-48d4-9213-7b142224f6b0") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("812c4430-2c72-49e7-8569-7abb5b2adf76"), new Guid("cdc70f49-6ec5-4cc3-bc6b-ec0d21652675") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("24da146e-0354-43bd-9e2a-dac1bf5c04fe"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("812c4430-2c72-49e7-8569-7abb5b2adf76"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8bc0903b-5f78-4723-a492-a2a825ae44f7"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f8a8d71c-1d62-4697-a4cb-989df1d76866"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3c32cbde-34f1-497a-a332-0d98187ce761"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("47c65afa-8621-4206-ad80-d065774bc33a"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4bdf852e-b8f5-48d4-9213-7b142224f6b0"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cdc70f49-6ec5-4cc3-bc6b-ec0d21652675"));
        }
    }
}
