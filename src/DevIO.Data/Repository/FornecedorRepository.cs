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
    public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository 
    {
        public FornecedorRepository(MeuDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<Fornecedor> ObterFornecedorEnderedo(Guid id)
        {
            return await Db.Fornecedores.AsNoTracking().Include(f=>f.Endereco)
                .FirstOrDefaultAsync(f=>f.Id == id);
        }

        public async Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id)
        {
            return await Db.Fornecedores.AsNoTracking()
                .Include(f=>f.Endereco)
                .Include(f=>f.Produtos)
                .FirstOrDefaultAsync(f=>f.Id ==id);

        }
    }
}
