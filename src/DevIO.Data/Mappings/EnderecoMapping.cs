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
    public class EnderecoMapping : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Logradouro)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(e => e.Numero)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(e => e.Cep)
                .IsRequired()
                .HasColumnType("varchar(8)");

            builder.Property(e => e.Complemento)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(e => e.Bairro)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(e => e.Cidade)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(e => e.Estado)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.ToTable("Enderecos");
        }
    }
}
