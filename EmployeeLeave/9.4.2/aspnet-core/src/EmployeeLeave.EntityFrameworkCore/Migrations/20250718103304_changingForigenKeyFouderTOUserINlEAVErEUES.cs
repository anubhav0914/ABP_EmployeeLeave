using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeLeave.Migrations
{
    /// <inheritdoc />
    public partial class changingForigenKeyFouderTOUserINlEAVErEUES : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequests_Founders_Founder_Id_approved_rejected_BY",
                table: "LeaveRequests");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequests_AbpUsers_Founder_Id_approved_rejected_BY",
                table: "LeaveRequests",
                column: "Founder_Id_approved_rejected_BY",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequests_AbpUsers_Founder_Id_approved_rejected_BY",
                table: "LeaveRequests");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequests_Founders_Founder_Id_approved_rejected_BY",
                table: "LeaveRequests",
                column: "Founder_Id_approved_rejected_BY",
                principalTable: "Founders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
