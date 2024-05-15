using System.ComponentModel.DataAnnotations;

namespace Capital_Placement_Test.DTOs.RequestDto
{
    public class CreateProgramDto
    {
        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Description { get; set; }
    }
}
