using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFrameworkExercise.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDeletedAtAndIsDeletedToProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "deleted_at",
                schema: "store",
                table: "product",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                schema: "store",
                table: "product",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_product_deleted_at",
                schema: "store",
                table: "product",
                column: "deleted_at");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropIndex(
                name: "IX_product_is_deleted",
                schema: "store",
                table: "product");

            migrationBuilder.DropColumn(
                name: "deleted_at",
                schema: "store",
                table: "product");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                schema: "store",
                table: "product");
        }
    }
}
