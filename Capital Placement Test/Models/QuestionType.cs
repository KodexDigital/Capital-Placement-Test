using cosmo_db_test.Enums;

namespace cosmo_db_test.Models
{
    public class QuestionType : BaseModel
    {
        public QuestionCategory Type { get; set; }
        public string? Question { get; set; }
    }
}