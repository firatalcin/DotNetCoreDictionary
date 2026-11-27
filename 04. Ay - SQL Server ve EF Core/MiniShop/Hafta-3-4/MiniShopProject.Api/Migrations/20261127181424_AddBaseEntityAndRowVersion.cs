using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniShopProject.Api.Migrations;

/// <inheritdoc />
public partial class _20261127181424_AddBaseEntityAndRowVersion : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_Customers_Email",
            table: "Customers");

        migrationBuilder.DropIndex(
            name: "IX_Categories_Name",
            table: "Categories");

        migrationBuilder.DropColumn(
            name: "CreatedAt",
            table: "Customers");

        migrationBuilder.AddColumn<DateTime>(
            name: "CreatedDate",
            table: "Products",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<bool>(
            name: "IsDeleted",
            table: "Products",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<DateTime>(
            name: "ModifiedDate",
            table: "Products",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<byte[]>(
            name: "RowVersion",
            table: "Products",
            type: "rowversion",
            rowVersion: true,
            nullable: false,
            defaultValue: new byte[0]);

        migrationBuilder.AddColumn<DateTime>(
            name: "CreatedDate",
            table: "Orders",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<bool>(
            name: "IsDeleted",
            table: "Orders",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<DateTime>(
            name: "ModifiedDate",
            table: "Orders",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "CreatedDate",
            table: "OrderItems",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<bool>(
            name: "IsDeleted",
            table: "OrderItems",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<DateTime>(
            name: "ModifiedDate",
            table: "OrderItems",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "CreatedDate",
            table: "Customers",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<bool>(
            name: "IsDeleted",
            table: "Customers",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<DateTime>(
            name: "ModifiedDate",
            table: "Customers",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "CreatedDate",
            table: "Categories",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<bool>(
            name: "IsDeleted",
            table: "Categories",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<DateTime>(
            name: "ModifiedDate",
            table: "Categories",
            type: "datetime2",
            nullable: true);

        migrationBuilder.CreateIndex(
            name: "IX_Customers_Email",
            table: "Customers",
            column: "Email",
            unique: true,
            filter: "[IsDeleted] = 0");

        migrationBuilder.CreateIndex(
            name: "IX_Categories_Name",
            table: "Categories",
            column: "Name",
            unique: true,
            filter: "[IsDeleted] = 0");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_Customers_Email",
            table: "Customers");

        migrationBuilder.DropIndex(
            name: "IX_Categories_Name",
            table: "Categories");

        migrationBuilder.DropColumn(
            name: "CreatedDate",
            table: "Products");

        migrationBuilder.DropColumn(
            name: "IsDeleted",
            table: "Products");

        migrationBuilder.DropColumn(
            name: "ModifiedDate",
            table: "Products");

        migrationBuilder.DropColumn(
            name: "RowVersion",
            table: "Products");

        migrationBuilder.DropColumn(
            name: "CreatedDate",
            table: "Orders");

        migrationBuilder.DropColumn(
            name: "IsDeleted",
            table: "Orders");

        migrationBuilder.DropColumn(
            name: "ModifiedDate",
            table: "Orders");

        migrationBuilder.DropColumn(
            name: "CreatedDate",
            table: "OrderItems");

        migrationBuilder.DropColumn(
            name: "IsDeleted",
            table: "OrderItems");

        migrationBuilder.DropColumn(
            name: "ModifiedDate",
            table: "OrderItems");

        migrationBuilder.DropColumn(
            name: "CreatedDate",
            table: "Customers");

        migrationBuilder.DropColumn(
            name: "IsDeleted",
            table: "Customers");

        migrationBuilder.DropColumn(
            name: "ModifiedDate",
            table: "Customers");

        migrationBuilder.DropColumn(
            name: "CreatedDate",
            table: "Categories");

        migrationBuilder.DropColumn(
            name: "IsDeleted",
            table: "Categories");

        migrationBuilder.DropColumn(
            name: "ModifiedDate",
            table: "Categories");

        migrationBuilder.AddColumn<DateTime>(
            name: "CreatedAt",
            table: "Customers",
            type: "datetime2",
            nullable: false,
            defaultValueSql: "SYSDATETIME()");

        migrationBuilder.CreateIndex(
            name: "IX_Customers_Email",
            table: "Customers",
            column: "Email",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Categories_Name",
            table: "Categories",
            column: "Name",
            unique: true);
    }
}
