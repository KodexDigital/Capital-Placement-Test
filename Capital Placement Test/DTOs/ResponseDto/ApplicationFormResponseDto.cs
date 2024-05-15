using Capital_Placement_Test.DTOs.RequestDto;
using Capital_Placement_Test.Models;

namespace Capital_Placement_Test.DTOs.ResponseDto
{
    public class ApplicationFormResponseDto : ApplicationForm
    {
        public List<QuestionsAndAnswersDto>? AdditionalQuestions { get; set; }
    }
}
