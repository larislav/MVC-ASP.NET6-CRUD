using Bogus;
using Bogus.DataSets;
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
            var genero = new Faker().PickRandom<Name.Gender>();

            var fornecedor = new Faker<Fornecedor>("pt_BR")
                .CustomInstantiator(f => new Fornecedor()
                {
                    Nome = $"{f.Name.FullName(genero)}",
                    Documento = "29433590090",
                    TipoFornecedor = TipoFornecedor.PessoaFisica,
                    Endereco = new Endereco
                    {
                        Cidade = f.Address.City(),
                        Bairro = f.Address.County(),
                        Estado = f.Address.State(),
                        Logradouro = f.Address.StreetName(),
                        Numero = f.Address.BuildingNumber(),
                        Cep = f.Address.ZipCode().Replace("-", "")
                    }
                });

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
            var genero = new Faker().PickRandom<Name.Gender>();

            var fornecedor = new Faker<Fornecedor>("pt_BR")
                .CustomInstantiator(f => new Fornecedor
                {
                    Nome = $"{f.Name.FullName}",
                    Documento = "73753999000113",
                    TipoFornecedor = TipoFornecedor.PessoaJuridica,
                    Endereco = new Endereco
                    {
                        Cep = f.Address.ZipCode().Replace("-", ""),
                        Cidade = f.Address.City(),
                        Bairro = f.Address.County(),
                        Estado = f.Address.State(),
                        Logradouro = f.Address.StreetName(),
                        Numero = f.Address.BuildingNumber()
                    }
                });

            return fornecedor;
        }

        public void Dispose()
        {
        }
    }
}
