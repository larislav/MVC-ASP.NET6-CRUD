using DevIO.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Data.Mappings
{
    public class FornecedorMapping : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Nome)
                .HasColumnType("varchar(200)");

            builder.Property(f => f.Documento)
                .IsRequired()
                .HasColumnType("varchar(14)");


            //mapeamento de relação 1 para 1  : Fornecedor para Endereço

            builder.HasOne(f => f.Endereco) //hasOne pois o fornecedor tem 1 endereço
                .WithOne(e => e.Fornecedor); //e o Endereço tem 1 fornecedor

            //mapeamento de relação 1 para N : Fornecedor tem vários Produtos

            builder.HasMany(f => f.Produtos)
                .WithOne(p => p.Fornecedor)
                .HasForeignKey(p => p.FornecedorId); //setar a chave estrangeira da tabela Produtos
                


            builder.ToTable("Fornecedores");
        }
    }

  
}
