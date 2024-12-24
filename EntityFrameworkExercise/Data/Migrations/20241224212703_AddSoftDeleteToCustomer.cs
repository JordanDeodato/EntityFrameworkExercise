using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFrameworkExercise.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSoftDeleteToCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                schema: "store",
                table: "customer",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "store",
                table: "customer",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_customer_IsDeleted",
                schema: "store",
                table: "customer",
                column: "IsDeleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_customer_IsDeleted",
                schema: "store",
                table: "customer");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "store",
                table: "customer");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "store",
                table: "customer");
        }
    }
}
