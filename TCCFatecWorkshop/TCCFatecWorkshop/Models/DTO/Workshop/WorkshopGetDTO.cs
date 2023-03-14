using System.ComponentModel.DataAnnotations;

namespace TCCFatecWorkshop.Models.DTO.Workshop
{
    public class WorkshopGetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string? Email { get; set; }

        public string? Description { get; set; }

        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
