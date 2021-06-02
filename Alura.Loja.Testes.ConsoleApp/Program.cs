using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Loja.Testes.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var contexto = new LojaContext())
            {
                var entries = contexto.ChangeTracker.Entries();
                var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();

                loggerFactory.AddProvider(SqlLoggerProvider.Create());
                loggerFactory.AddProvider(SqlLoggerProvider.Create());

                var produtos = contexto.Produtos.ToList();

                //Exibir(produtos);

                ExibirEntries(entries);

                var p1 = produtos.First();
                //p1.Nome = "Harry Potter 2";
                var novoProduto = new Produto();
                novoProduto.Nome = "Salgadinho";
                novoProduto.Categoria = "Alimento";
                novoProduto.Preco = 5.99;
                contexto.Produtos.Add(novoProduto);
                ExibirEntries(entries);
                var entry = contexto.Entry(novoProduto);
                contexto.Produtos.Remove(novoProduto);
                Console.WriteLine("\n\n"+entry.Entity.ToString() + " - " + entry.State);



                //contexto.SaveChanges();
                ExibirEntries(entries);
                //Exibir(produtos);


            }

            Console.ReadKey();
        }

        private static void ExibirEntries(IEnumerable<EntityEntry> entries)
        {
            Console.WriteLine("======================================================================================");
            foreach (var entrie in entries)
            {
                Console.WriteLine($"{entrie.Entity.ToString()} - {entrie.State}");
                
            }
        }

        private static void Exibir<T>(IEnumerable<T> produtos)
        {
            Console.WriteLine("======================================================================================");
            foreach (var item in produtos)
            {
                Console.WriteLine(item);
            }
        }

        
    }
}
