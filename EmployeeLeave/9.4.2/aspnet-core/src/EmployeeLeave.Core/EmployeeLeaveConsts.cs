using EmployeeLeave.Debugging;

namespace EmployeeLeave
{
    public class EmployeeLeaveConsts
    {
        public const string LocalizationSourceName = "EmployeeLeave";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "fb3c1c5ad76140edb4a8fb994e715736";
    }
}
