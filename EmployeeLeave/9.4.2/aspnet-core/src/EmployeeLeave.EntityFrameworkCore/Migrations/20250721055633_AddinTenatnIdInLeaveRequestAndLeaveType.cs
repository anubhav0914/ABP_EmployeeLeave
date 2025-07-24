﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeLeave.Migrations
{
    /// <inheritdoc />
    public partial class AddinTenatnIdInLeaveRequestAndLeaveType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "LeaveTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "LeaveRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "LeaveTypes");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "LeaveRequests");
        }
    }
}
