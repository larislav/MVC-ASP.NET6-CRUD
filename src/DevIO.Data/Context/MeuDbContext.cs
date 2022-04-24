using DevIO.Business.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Data.Context
{
    public class MeuDbContext : DbContext
    {
        public MeuDbContext(DbContextOptions options) : base(options)   
        {

        }

        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Produto> Produtos { get; set; }


        //método que vai ser chamado durante a criação desse modelo no banco
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e=>e.GetProperties()
                .Where(p=>p.ClrType == typeof(string))))
            {
                property.SetColumnType("varchar(100)");
            }

            //Pelo DbContext, vai buscar todas as entidades mapeadas nele
            //e vai buscar classes que herdem de IEntityTypeConfiguration para 
            //aquelas entidades que estao relacionadas no DbContext
            //e vai registra-las de uma vez só
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeuDbContext).Assembly);

            //desabilitar o cascade delete para o caso de um fornecedor ser excluido,
            //acaba excluindo todos os produtos também
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e=>e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
