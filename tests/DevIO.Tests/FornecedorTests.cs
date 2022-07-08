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
        public FornecedorTests(FornecedorTestsFixture fornecedorTestsFixture)
        {
            _fornecedorTestsFixture = fornecedorTestsFixture;
        }

        [Fact]
        public void Adicionar_FornecedorInvalido_DeveFalhar()
        {
            //Arrange
            var fornecedor = _fornecedorTestsFixture.GerarFornecedorInvalido();
            var mocker = new AutoMocker();
            var fornecedorService = mocker.CreateInstance<FornecedorService>();

            //Act
            fornecedorService.Adicionar(fornecedor);

            //Assert
            Assert.False(fornecedorService.ExecutarValidacao(new FornecedorValidation(), fornecedor));
            Assert.False(fornecedorService.ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco));
            mocker.GetMock<IFornecedorRepository>().Verify(r => r.Adicionar(fornecedor), Times.Never);
        }

        [Fact]
        public void Adicionar_FornecedorPFValido_DeveAdicionarComSucesso()
        {
            //Arrange
            var fornecedor = _fornecedorTestsFixture.GerarFornecedorPFValido();
            var mocker = new AutoMocker();
            var fornecedorService = mocker.CreateInstance<FornecedorService>();

            //Act
            fornecedorService.Adicionar(fornecedor);

            //Assert
            Assert.True(fornecedorService.ExecutarValidacao(new FornecedorValidation(), fornecedor));
            Assert.True(fornecedorService.ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco));
            mocker.GetMock<IFornecedorRepository>().Verify(r => r.Adicionar(fornecedor), Times.Once);

        }

        [Fact]
        public void Adicionar_FornecedorPJValido_DeveAdicionarComSucesso()
        {
            //Arrange
            var fornecedor = _fornecedorTestsFixture.GerarFornecedorPJValido();
            var mocker = new AutoMocker();
            var fornecedorService = mocker.CreateInstance<FornecedorService>();

            //Act
            fornecedorService.Adicionar(fornecedor);

            //Assert
            Assert.True(fornecedorService.ExecutarValidacao(new FornecedorValidation(), fornecedor));
            Assert.True(fornecedorService.ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco));
            mocker.GetMock<IFornecedorRepository>().Verify(r => r.Adicionar(fornecedor), Times.Once);

        }

        [Fact]
        public void Remover_GuidFornecedorInvalido_DeveFalhar()
        {
            //Arrange
            var mocker = new AutoMocker();
            var fornecedorService = mocker.CreateInstance<FornecedorService>();

            //Act
            fornecedorService.Remover(new Guid());

            //Assert
            mocker.GetMock<IFornecedorRepository>().Verify(r => r.Remover(new Guid()), Times.Never);
        }

        [Fact]
        public void Atualizar_FornecedorPFInvalido_DeveFalhar()
        {
            //Arrange
            var fornecedor = _fornecedorTestsFixture.GerarFornecedorInvalido();
            var mocker = new AutoMocker();
            var fornecedorService = mocker.CreateInstance<FornecedorService>();

            //Act
            fornecedorService.Atualizar(fornecedor);

            //Assert
            //Assert.False(fornecedorService.ExecutarValidacao(new FornecedorValidation(), fornecedor));
            mocker.GetMock<IFornecedorRepository>().Verify(r => r.Remover(new Guid()), Times.Never);
        }

        [Fact]
        public void Atualizar_FornecedorPFValido_DeveAtualizarComSucesso()
        {
            //Arrange
            var fornecedor = _fornecedorTestsFixture.GerarFornecedorPFValido();
            var mocker = new AutoMocker();
            var fornecedorService = mocker.CreateInstance<FornecedorService>();

            //Act
            fornecedorService.Atualizar(fornecedor);

            //Assert
            Assert.True(fornecedorService.ExecutarValidacao(new FornecedorValidation(), fornecedor));
            mocker.GetMock<IFornecedorRepository>().Verify(r => r.Atualizar(fornecedor), Times.Once);
        }
    }
   
}
