using System.ComponentModel.DataAnnotations;

namespace TCCFatecWorkshop.Models.DTO.Product
{
    public class ProductDTO
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "O campo Nome deve ter entre 3 e 150 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo Categoria é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo Categoria deve ter entre 3 e 100 caracteres.")]
        public string Category { get; set; }

        [Required(ErrorMessage = "O campo Preço é obrigatório.")]
        [Range(typeof(decimal), "0.00", "100000.00", ErrorMessage = "O campo Preço deve ter no máximo de R$ 100000.")]
        public double SalePrice { get; set; }

        [StringLength(1000, ErrorMessage = "O campo Descrição deve ter no máximo 1000 caracteres.")]
        public string? Description { get; set; }
    }
}
