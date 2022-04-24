using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Data.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {

        public ProdutoRepository(MeuDbContext context) : base(context)
        {

        }
        public async Task<Produto> ObterProdutoFornecedor(Guid id)
        {
            //inclue o fornecedor no result fazendo um join com o fornecedor,
            //onde o produto tem esse Id
            return await Db.Produtos.AsNoTracking().Include(f=>f.Fornecedor)
                .FirstOrDefaultAsync(p=>p.Id == id);
        }

        public async Task<IEnumerable<Produto>> ObterProdutosEFornecedores()
        {
            return await Db.Produtos.AsNoTracking().Include(f => f.Fornecedor)
                .OrderBy(p => p.Nome).ToListAsync();
        }

        public async Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId)
        {
            return await Buscar(p => p.FornecedorId == fornecedorId);
        }
    }
}
