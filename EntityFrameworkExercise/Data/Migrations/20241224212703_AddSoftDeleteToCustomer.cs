﻿using System;
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
                name: "deleted_at",
                schema: "store",
                table: "customer",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                schema: "store",
                table: "customer",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_customer_is_deleted",
                schema: "store",
                table: "customer",
                column: "deleted_at");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_customer_is_deleted",
                schema: "store",
                table: "customer");

            migrationBuilder.DropColumn(
                name: "deleted_at",
                schema: "store",
                table: "customer");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                schema: "store",
                table: "customer");
        }
    }
}
