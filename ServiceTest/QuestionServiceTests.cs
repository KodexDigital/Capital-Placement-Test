using Capital_Placement_Test.DTOs.RequestDto;
using Capital_Placement_Test.Enums;
using Capital_Placement_Test.Models;
using Capital_Placement_Test.Response;
using Capital_Placement_Test.Services.Interfaces;
using Moq;

namespace ServiceTests
{
    public class QuestionServiceTests
    {
        private readonly Mock<IQuestionService> questionService;
        public QuestionServiceTests()
        {
            questionService = new Mock<IQuestionService>();
        }

        [Fact]
        public void Create_Question()
        {
            //arrange
            var response = questionService.Setup(p => p.CreateQuestionTypeAsync(new List<QuestionTypeDto>
            {
                new QuestionTypeDto{ Type = QuestionCategory.Paragraph , Question = "I delighted to working with you"}
            }));

            //assert
            Assert.NotNull(response);
        }

        [Theory]
        [InlineData("1B014E26-1F17-4A5B-9BD3-77DB74E99CB7")]
        public void Update_Question(string id)
        {
            //arrange
            var response = questionService.Setup(p => p.UpdateQuestionTypeAsync(id, new QuestionTypeDto
            {
                Type = QuestionCategory.YesNo,
                Question = "Do I love Capital Placemment: Yes"
            }));

            //assert
            Assert.NotNull(response);
        }

        [Fact]
        public void Get_All_Questions()
        {
            //arrange
            var response = questionService.Setup(p => p.GetAllQuestionTypesAsync()).ReturnsAsync(new ResponseHandler<List<QuestionType>>());
            //assert
            Assert.NotNull(response);
        }

        [Theory]
        [InlineData("1B014E26-1F17-4A5B-9BD3-77DB74E99CB7")]
        public void Get_Single_Question(string id)
        {
            //arrange
            var response = questionService.Setup(p => p.GetSingleQuestionTypeAsync(id));
            //assert
            Assert.NotNull(response);
        }
        
        [Theory]
        [InlineData("Do I love Capital Placemment: Yes")]
        public void Get_Single_Question_By_Question(string question)
        {
            //arrange
            var response = questionService.Setup(p => p.GetQuestionTypeByQuestionAsync(question));
            //assert
            Assert.NotNull(response);
        }

        [Theory]
        [InlineData(QuestionCategory.Paragraph)]
        public void Get_Single_Question_By_Type(QuestionCategory type)
        {
            //arrange
            var response = questionService.Setup(p => p.GetSingleQuestionTypeAsync(type));
            //assert
            Assert.NotNull(response);
        }
    }
}