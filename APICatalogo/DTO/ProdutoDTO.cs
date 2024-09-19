using APICatalogo.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APICatalogo.DTO
{
    public class ProdutoDTO
    {
        public int ProdutoId { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório!")]
        [StringLength(80)]
        //[StringLength(80, ErrorMessage = "O nome deve ter entre 5 a 20 caracters", MinimumLength = 5)]
        //[PrimeiraLetraMaiuscula]
        public string? Nome { get; set; }

        [Required]
        [MaxLength(100)]
        //[StringLength(10, ErrorMessage ="A descrição deve ter no máximo {1} carateres")]
        public string? Descricao { get; set; }

        [Required]      
        public decimal Preco { get; set; }

        [Required]
        //[StringLength(300, MinimumLength = 10)]
        public string? ImagemUrl { get; set; }

        public int CategoriaId { get; set; }

    }
}
