using Microsoft.EntityFrameworkCore;
using System;

namespace Alura.Loja.Testes.ConsoleApp
{
    public class LojaContext : DbContext
    {
        //Informar aqui quais classes serão persistidas no banco
        public DbSet<Produto> Produtos { get; set; } 
        public DbSet<Compra> Compras { get; set; } 
        public DbSet<Promocao> Promocao { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PromocaoProduto>().HasKey(pp => new { pp.PromocaoId, pp.ProdutoId });

            modelBuilder.Entity<Endereco>().ToTable("Enderecos");

            modelBuilder.Entity<Endereco>().Property<int>("ClienteId");
            modelBuilder.Entity<Endereco>().HasKey("ClienteId");



        }


        //Método para configurar o banco de dados
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Configura a conexão com o banco - Diz para a classe LojaContext que quer usar o SQLServer.
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=LojaDB;Trusted_Connection=true;");
        }
    }
}