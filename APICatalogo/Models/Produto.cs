using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalogo.Models;

[Table("Produtos")]
public class Produto
{
    [Key]
    public int ProdutoId { get; set; }

    [Required]
    [StringLength(80)]
    //[StringLength(80, ErrorMessage = "O nome deve ter entre 5 a 20 caracters", MinimumLength = 5)]
    //[PrimeiraLetraMaiuscula]
    public string? Nome { get; set; }

    [Required]
    [MaxLength(300)]
    //[StringLength(10, ErrorMessage ="A descrição deve ter no máximo {1} carateres")]
    public string? Descricao { get; set; }

    [Required]
    //[Range(1, 1000, ErrorMessage = "O preço deve estar entre {1} e {2}")]
    [Column(TypeName = "decimal()10,")]
    public decimal Preco { get; set; }

    [Required]
    [StringLength(300)]
    public string? ImagemUrl { get; set; }
    public float Estoque { get; set; }
    public DateTime DataCadastro { get; set; }
    public int CategoriaId { get; set; }

    [JsonIgnore]
    public Categoria? Categoria { get; set; }

   // public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
   // {
   //     if(this.Estoque <= 0)
    //    {
    //        yield return new ValidationResult("O estoque deve ser maior que zero!", new[]
    //                {
    //                    nameof(this.Estoque)
     //               });
    //    }
    //}
}
