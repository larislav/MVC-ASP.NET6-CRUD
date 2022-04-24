using DevIO.App.Extensions;
using System.ComponentModel.DataAnnotations;

namespace DevIO.App.ViewModels
{
    public class ProdutoViewModel
    {
        [Key] // para informar que vai ser uma chave e não um campo
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Fornecedor")]
        public Guid FornecedorId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Descrição")]
        [StringLength (1000, ErrorMessage ="O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Descricao { get; set; }

        [Display(Name = "Imagem do Produto")]
        public IFormFile ImagemUpload { get; set; }

        public string Imagem { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Moeda]
        public decimal Valor { get; set; } // tipo mais apropriado para valor

        [ScaffoldColumn(false)] //Não considerar essa coluna quando fizer scaffold
        public DateTime DataCadastro { get; set; }

        [Display(Name ="Ativo?")]
        public bool Ativo { get; set; }

        public FornecedorViewModel Fornecedor { get; set; }
        public IEnumerable<FornecedorViewModel> Fornecedores { get; set; }

    }
}
