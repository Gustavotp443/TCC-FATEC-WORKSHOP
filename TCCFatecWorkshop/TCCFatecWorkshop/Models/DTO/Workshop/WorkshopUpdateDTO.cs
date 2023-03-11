using System.ComponentModel.DataAnnotations;

namespace TCCFatecWorkshop.Models.DTO.Workshop
{
    public class WorkshopUpdateDTO
    {
        [StringLength(150, MinimumLength = 3, ErrorMessage = "O campo Nome deve ter entre 3 e 150 caracteres.")]
        public string? Name { get; set; }

        [EmailAddress(ErrorMessage = "O campo Email deve ser um endereço de email válido.")]
        public string? Email { get; set; }

        [StringLength(1000, ErrorMessage = "O campo Description deve ter até 1000 caracteres.")]
        public string? Description { get; set; }
    }
}
