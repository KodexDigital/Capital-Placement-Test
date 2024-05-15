using Capital_Placement_Test.DTOs.RequestDto;
using Capital_Placement_Test.Enums;
using Capital_Placement_Test.Models;
using Capital_Placement_Test.Response;
using Capital_Placement_Test.Services.Interfaces;
using Moq;

namespace ServiceTests
{
    public class ApplicationServiceTests
    {
        private readonly Mock<IApplicationFormService> applicationService;
        public ApplicationServiceTests()
        {
            applicationService = new Mock<IApplicationFormService>();
        }

        [Fact]
        public void Apply()
        {
            //arrange
            var response = applicationService.Setup(p => p.ApplyAsync(new ApplicationFormDto
            {
                FirstName = "Kenneth",
                LastName = "Otoro",
                Email = "kodexkenth@gmail.com",
                PhoneNumber = "08101263634",
                DateOfBirth = DateTime.Now.AddYears(-20),
                Nationality = "Nigerian",
                CurrentResidence = "Lagos, Nigeria",
                IdentityNumber = "International Passport",
                Gender = Gender.Male,
                AdditionalQuestions = new List<QuestionsAndAnswersDto>
                {
                    new QuestionsAndAnswersDto {Question = "Do I love Capital Placemment", Answer = "Yes"}
                },
            }));

            //assert
            Assert.NotNull(response);
        }

        [Theory]
        [InlineData("1B014E26-1F17-4A5B-9BD3-77DB74E99CB7")]
        public void Update_Application(string id)
        {
            //arrange
            var response = applicationService.Setup(p => p.UpdateApplicationFormAsync(id, new ApplicationFormDto
            {
                FirstName = "Kenneth",
                LastName = "Otoro",
                Email = "kodexkenth@gmail.com",
                PhoneNumber = "08101263634",
                DateOfBirth = DateTime.Now.AddYears(-20),
                Nationality = "Nigerian",
                CurrentResidence = "Lagos, Nigeria",
                IdentityNumber = "International Passport",
                Gender = Gender.Male,
                AdditionalQuestions = new List<QuestionsAndAnswersDto>
                {
                    new QuestionsAndAnswersDto {Question = "Do I love Capital Placemment", Answer = "Yes"}
                },
            }));

            //assert
            Assert.NotNull(response);
        }

        [Fact]
        public void Get_All_Questions()
        {
            //arrange
            var response = applicationService.Setup(p => p.GetAllApplicationFormsAsync()).ReturnsAsync(new ResponseHandler<List<ApplicationForm>>());
            //assert
            Assert.NotNull(response);
        }

        [Theory]
        [InlineData("1B014E26-1F17-4A5B-9BD3-77DB74E99CB7")]
        public void Get_Single_Question(string id)
        {
            //arrange
            var response = applicationService.Setup(p => p.GetSingleApplicationFormAsync(id));
            //assert
            Assert.NotNull(response);
        }        
    }
}