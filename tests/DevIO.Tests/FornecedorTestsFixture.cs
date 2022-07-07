using DevIO.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DevIO.Tests
{
    [CollectionDefinition(nameof(FornecedorCollection))]

    public class FornecedorCollection : ICollectionFixture<FornecedorTestsFixture>
    {
    }

    public class FornecedorTestsFixture : IDisposable
    {
        public Fornecedor GerarFornecedorPFValido()
        {
            var fornecedor = new Fornecedor
            {
                Nome = "Eddie Munson",
                Documento = "29433590090",
                TipoFornecedor = TipoFornecedor.PessoaFisica,
                Endereco = new Endereco
                {
                    Logradouro = "Stranger",
                    Bairro = "Things",
                    Cep = "50000000",
                    Cidade = "Hawkins",
                    Estado = "Indiana",
                    Numero = "011"
                }

            };

            return fornecedor;
        }

        public Fornecedor GerarFornecedorInvalido()
        {
            var fornecedor = new Fornecedor();
            fornecedor.Endereco = new Endereco();

            return fornecedor;
        }

        public Fornecedor GerarFornecedorPJValido()
        {
            var fornecedor = new Fornecedor
            {
                Nome = "Eddie Munson",
                Documento = "73753999000113",
                TipoFornecedor = TipoFornecedor.PessoaJuridica,
                Endereco = new Endereco
                {
                    Logradouro = "Stranger",
                    Bairro = "Things",
                    Cep = "50000000",
                    Cidade = "Hawkins",
                    Estado = "Indiana",
                    Numero = "011"
                }

            };

            return fornecedor;
        }

        public void Dispose()
        {
        }
    }
}
