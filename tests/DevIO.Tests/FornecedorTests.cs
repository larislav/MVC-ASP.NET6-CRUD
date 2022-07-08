using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;
using DevIO.Business.Notificacoes;
using DevIO.Business.Services;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DevIO.Tests
{
    [Collection(nameof(FornecedorCollection))]
    public class FornecedorTests 
    {
        private readonly FornecedorTestsFixture _fornecedorTestsFixture;
        private readonly FornecedorService _fornecedorService;
        public FornecedorTests(FornecedorTestsFixture fornecedorTestsFixture)
        {
            _fornecedorTestsFixture = fornecedorTestsFixture;
            _fornecedorService = _fornecedorTestsFixture.ObterFornecedorService();
        }

        [Fact]
        public void Adicionar_FornecedorInvalido_DeveFalhar()
        {
            //Arrange
            var fornecedor = _fornecedorTestsFixture.GerarFornecedorInvalido();

            //Act
            _fornecedorService.Adicionar(fornecedor);

            //Assert
            Assert.False(_fornecedorService.ExecutarValidacao(new FornecedorValidation(), fornecedor));
            Assert.False(_fornecedorService.ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco));
            _fornecedorTestsFixture.Mocker.GetMock<IFornecedorRepository>().Verify(r => r.Adicionar(fornecedor), Times.Never);
        }

        [Fact]
        public void Adicionar_FornecedorPFValido_DeveAdicionarComSucesso()
        {
            //Arrange
            var fornecedor = _fornecedorTestsFixture.GerarFornecedorPFValido();

            //Act
            _fornecedorService.Adicionar(fornecedor);

            //Assert
            Assert.True(_fornecedorService.ExecutarValidacao(new FornecedorValidation(), fornecedor));
            Assert.True(_fornecedorService.ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco));
            _fornecedorTestsFixture.Mocker.GetMock<IFornecedorRepository>().Verify(r => r.Adicionar(fornecedor), Times.Once);

        }

        [Fact]
        public void Adicionar_FornecedorPJValido_DeveAdicionarComSucesso()
        {
            //Arrange
            var fornecedor = _fornecedorTestsFixture.GerarFornecedorPJValido();

            //Act
            _fornecedorService.Adicionar(fornecedor);

            //Assert
            Assert.True(_fornecedorService.ExecutarValidacao(new FornecedorValidation(), fornecedor));
            Assert.True(_fornecedorService.ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco));
            _fornecedorTestsFixture.Mocker.GetMock<IFornecedorRepository>().Verify(r => r.Adicionar(fornecedor), Times.Once);

        }

        [Fact]
        public void Remover_GuidFornecedorInvalido_DeveFalhar()
        {
            //Arrange
            Guid guid = Guid.NewGuid();

            //Act
            _fornecedorService.Remover(guid);

            //Assert
            _fornecedorTestsFixture.Mocker.GetMock<IFornecedorRepository>().Verify(r => r.Remover(guid), Times.Never);
        }

        [Fact]
        public void Atualizar_FornecedorPFInvalido_DeveFalhar()
        {
            //Arrange
            var fornecedor = _fornecedorTestsFixture.GerarFornecedorInvalido();

            //Act
            _fornecedorService.Atualizar(fornecedor);

            //Assert
            _fornecedorTestsFixture.Mocker.GetMock<IFornecedorRepository>().Verify(r => r.Remover(new Guid()), Times.Never);
        }

        [Fact]
        public void Atualizar_FornecedorPFValido_DeveAtualizarComSucesso()
        {
            //Arrange
            var fornecedor = _fornecedorTestsFixture.GerarFornecedorPFValido();

            //Act
            _fornecedorService.Atualizar(fornecedor);

            //Assert
            Assert.True(_fornecedorService.ExecutarValidacao(new FornecedorValidation(), fornecedor));
            _fornecedorTestsFixture.Mocker.GetMock<IFornecedorRepository>().Verify(r => r.Atualizar(fornecedor), Times.Once);
        }
    }
   
}
