using System.ComponentModel.DataAnnotations;

namespace EmployeeLeave.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}