using cosmo_db_test.DTOs.RequestDto;
using cosmo_db_test.Models;

namespace cosmo_db_test.DTOs.ResponseDto
{
    public class ApplicationFormResponseDto : ApplicationForm
    {
        public List<QuestionsAndAnswersDto>? AdditionalQuestions { get; set; }
    }
}
