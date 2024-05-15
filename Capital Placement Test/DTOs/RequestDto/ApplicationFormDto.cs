using Capital_Placement_Test.Enums;
using System.ComponentModel.DataAnnotations;

namespace Capital_Placement_Test.DTOs.RequestDto
{
    public class ApplicationFormDto
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Nationality { get; set; }
        public string? CurrentResidence { get; set; }
        public string? IdentityNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Gender? Gender { get; set; }
        public List<QuestionsAndAnswersDto>? AdditionalQuestions { get; set; }
    }

    public class QuestionsAndAnswersDto
    {
        public string? Question { get; set;}
        public string? Answer { get; set;}
    }
}