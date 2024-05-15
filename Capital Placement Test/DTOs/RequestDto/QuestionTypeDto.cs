using cosmo_db_test.Enums;
using System.ComponentModel.DataAnnotations;

namespace cosmo_db_test.DTOs.RequestDto
{
    public class QuestionTypeDto
    {
        [Required]
        public QuestionCategory Type { get; set; }

        [Required]
        public string? Question { get; set; }
    }
}
