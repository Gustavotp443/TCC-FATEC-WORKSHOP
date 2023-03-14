using System.ComponentModel.DataAnnotations;

namespace TCCFatecWorkshop.Models.DTO.User
{
    public class UserGetDTO
    {    
        public string Username { get; set; }

        public string Email { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set;}
    }
}
