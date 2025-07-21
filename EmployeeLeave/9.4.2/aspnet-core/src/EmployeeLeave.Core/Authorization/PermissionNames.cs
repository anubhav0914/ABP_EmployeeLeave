namespace EmployeeLeave.Authorization
{
    public static class PermissionNames
    {
        public const string Pages_Tenants = "Pages.Tenants";

        public const string Pages_Users = "Pages.Users";
        public const string Pages_Users_Activation = "Pages.Users.Activation";

        public const string Pages_Roles = "Pages.Roles";


        // Employee permissions
        public const string Employee_Create = "Employee.Create";
        public const string Employee_Update = "Employee.Update";
        public const string Employee_Delete = "Employee.Delete";
        public const string Employee_View =   "Employee.View";

        // LeaveType permissions
        public const string LeaveType_Create = "LeaveType.Create";
        public const string LeaveType_Update = "LeaveType.Update";
        public const string LeaveType_Delete = "LeaveType.Delete";
        public const string LeaveType_View = "LeaveType.View";

        // LeaveRequest permissions
        public const string LeaveRequest_Create = "LeaveRequest.Create";
        public const string LeaveRequest_ApproveReject = "LeaveRequest.ApproveReject";
        public const string LeaveRequest_View_All = "LeaveRequest.View.All";
        public const string LeaveRequest_View_Own = "LeaveRequest.View.Own";
        public const string LeaveRequest_Cancel_Own = "LeaveRequest.Cancel.Own";
    }
}