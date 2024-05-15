using Capital_Placement_Test.Enums;

namespace Capital_Placement_Test.Models
{
    public class QuestionType : BaseModel
    {
        public QuestionCategory Type { get; set; }
        public string? Question { get; set; }
    }
}