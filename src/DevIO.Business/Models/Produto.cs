using System.ComponentModel.DataAnnotations;

namespace DevIO.Business.Models
{
    public class Produto : Entity
    {
        public Guid FornecedorId { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public string Imagem { get; set; }

        public decimal Valor { get; set; } // tipo mais apropriado para valor
        public DateTime DataCadastro { get; set; }

        public bool Ativo { get; set; }

        //Relacionamentos Entity Framework

        public Fornecedor Fornecedor { get; set; }

        //isso diz para o EF que: tem uma relação com Fornecedor,
        //e o Fornecedor tem uma relação com Produto,
        //porém o Fornecedor tem Muitos Produtos,
        //enquanto Produto  tem apenas 1 fornecedor
        //estabelecendo o relacionamento de 1 para N

    }
}
