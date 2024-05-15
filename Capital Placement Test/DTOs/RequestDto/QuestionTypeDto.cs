using Capital_Placement_Test.Enums;
using System.ComponentModel.DataAnnotations;

namespace Capital_Placement_Test.DTOs.RequestDto
{
    public class QuestionTypeDto
    {
        [Required]
        public QuestionCategory Type { get; set; }

        [Required]
        public string? Question { get; set; }
    }
}
