using DevIO.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Interfaces
{
    //Pattern Repository para criar um repositório genérico
    //que consiste em oferecer todos os métodos necessários para qualquer
    //entidade

    //Implementa o IDisposable para obrigar o repositório a 
    //fazer o dispose para liberar memória
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        //Trabalhar com métodos assíncronos para garantir melhor performance
        //da aplicação 

        Task Adicionar(TEntity entity);
        Task<TEntity> ObterPorId(Guid id);
        Task<List<TEntity>> ObterTodos();
        Task Atualizar(TEntity entity);
        Task Remover(Guid id);

        //possibilitar passar uma expressao lambda por parametro para 
        //buscar qualquer entidade por qualquer parâmetro
        Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);

        Task<int> SaveChanges(); //retorna sempre um int que é o número de linhas afetadas

    }
}
