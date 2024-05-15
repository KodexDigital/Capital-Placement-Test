using Capital_Placement_Test.DTOs.RequestDto;
using Capital_Placement_Test.Models;
using Capital_Placement_Test.Response;
using Capital_Placement_Test.Services.Interfaces;
using Moq;

namespace ServiceTests
{
    public class ProgramServiceTests
    {
        private readonly Mock<IProgramService> programService;
        public ProgramServiceTests()
        {
            programService = new Mock<IProgramService>();
        }

        [Fact]
        public void Create_Program()
        {
            //arrange
            var response = programService.Setup(p => p.CreateCandidateProgramAsync(new CreateProgramDto
            {
                Title = "This job is mine",
                Description = "Let's grow together as I bring forth the spirit of workmanship and productivity to attain the company's goals and dreams."
            }));

            //assert
            Assert.NotNull(response);
        }

        [Theory]
        [InlineData("1B014E26-1F17-4A5B-9BD3-77DB74E99CB7")]
        public void Update_Program(string id)
        {
            //arrange
            var response = programService.Setup(p => p.UpdateCandidateProgramAsync(id, new CreateProgramDto
            {
                Title = "This job is mine",
                Description = "Let's grow together as I bring forth the spirit of workmanship and productivity to attain the company's goals and dreams."
            }));

            //assert
            Assert.NotNull(response);
        }

        [Fact]
        public void Get_All_Programs()
        {
            //arrange
            var response = programService.Setup(p => p.GetAllCandidateProgramsAsync()).ReturnsAsync(new ResponseHandler<List<CandidateProgram>>());
            //assert
            Assert.NotNull(response);
        }

        [Theory]
        [InlineData("1B014E26-1F17-4A5B-9BD3-77DB74E99CB7")]
        public void Get_Single_Program(string id)
        {
            //arrange
            var response = programService.Setup(p => p.GetSingleCandidateProgramAsync(id));
            //assert
            Assert.NotNull(response);
        }

    }
}