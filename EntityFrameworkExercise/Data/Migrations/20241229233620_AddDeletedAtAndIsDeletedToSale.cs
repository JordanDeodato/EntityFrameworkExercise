using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFrameworkExercise.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDeletedAtAndIsDeletedToSale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "deleted_at",
                schema: "store",
                table: "sale",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                schema: "store",
                table: "sale",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
               name: "IX_sale_deleted_at",
               schema: "store",
               table: "sale",
               column: "deleted_at");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "deleted_at",
                schema: "store",
                table: "sale");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                schema: "store",
                table: "sale");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                schema: "store",
                table: "sale");
        }
    }
}
