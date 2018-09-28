using System.ComponentModel.DataAnnotations;

namespace BoilerPlateSPA.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}