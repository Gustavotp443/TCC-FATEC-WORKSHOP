using System.ComponentModel.DataAnnotations;

namespace TCCFatecWorkshop.Models.DTO.User
{
    public class UserUpdateDTO
    {
            [StringLength(30, MinimumLength = 3, ErrorMessage = "O campo Nome de Usuário deve ter entre 3 e 30 caracteres.")]
            public string? Username { get; set; }

            [StringLength(30, MinimumLength = 6, ErrorMessage = "O campo Senha deve ter entre 6 e 30 caracteres.")]
            public string? Password { get; set; }       
    }
}
