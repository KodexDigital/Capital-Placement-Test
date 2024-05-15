using System.ComponentModel.DataAnnotations;

namespace cosmo_db_test.DTOs.RequestDto
{
    public class CreateProgramDto
    {
        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Description { get; set; }
    }
}
