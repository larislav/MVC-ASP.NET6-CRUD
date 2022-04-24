using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Data.Repository
{
    //só pode ser herdada, não pode ser instanciada
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        //Protected porque tanto o repository quanto quem herdar de Repository
        //vão poder ter acesso ao DbContext
        protected readonly MeuDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(MeuDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            //O tracking do entity Framework: toda vez que você coloca algo na memória
            // ele começa a rastrear esse objeto, para perceber mudanças de estado etc.
            //Porem se você faz a leitura desse objeto sem intenção de devolver ele
            //para base, apenas para ler, ele fica no tracking, ou seja a consulta gasta mais
            //vc retorna a resposta do banco porem sem o tracking para melhorar performance
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
            //o await espera o resultado acontecer e retorna o objeto transformado
            //na informaçao que vc espera
        }

        public virtual async Task<TEntity> ObterPorId(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<List<TEntity>> ObterTodos()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task Adicionar(TEntity entity)
        {
            //Db.Set<TEntity>().Add(entity);
            DbSet.Add(entity);
            await SaveChanges();
        }

        public virtual async Task Atualizar(TEntity entity)
        {
            Db.Entry(entity).State = EntityState.Modified;
            DbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task Remover(Guid id)
        {
            var entity = new TEntity() { Id = id };
            DbSet.Remove(entity);
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public async void Dispose()
        {
            Db?.Dispose();
        }
    }
}
