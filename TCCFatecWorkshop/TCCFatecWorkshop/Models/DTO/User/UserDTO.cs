using System.ComponentModel.DataAnnotations;

namespace TCCFatecWorkshop.Models.DTO.User
{
    public class UserDTO
    {
        [Required(ErrorMessage = "O campo Nome de Usuário é obrigatório.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "O campo Nome de Usuário deve ter entre 3 e 30 caracteres.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo Email deve ser um endereço de email válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "O campo Senha deve ter entre 6 e 30 caracteres.")]
        public string Password { get; set; }

    }
}
