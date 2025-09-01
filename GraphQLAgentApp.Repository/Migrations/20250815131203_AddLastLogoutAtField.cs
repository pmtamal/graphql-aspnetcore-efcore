﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraphQLAgentApp.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddLastLogoutAtField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastLogoutAt",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastLogoutAt",
                table: "Users");
        }
    }
}
