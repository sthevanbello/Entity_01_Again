using Microsoft.EntityFrameworkCore;
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
            #region ChangeTracker
            //using (var contexto = new LojaContext())
            //{
            //    var entries = contexto.ChangeTracker.Entries();
            //    var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
            //    var loggerFactory = serviceProvider.GetService<ILoggerFactory>();

            //    loggerFactory.AddProvider(SqlLoggerProvider.Create());
            //    loggerFactory.AddProvider(SqlLoggerProvider.Create());

            //    var produtos = contexto.Produtos.ToList();

            //    //Exibir(produtos);

            //    ExibirEntries(entries);

            //    var p1 = produtos.First();
            //    //p1.Nome = "Harry Potter 2";
            //    var novoProduto = new Produto();
            //    novoProduto.Nome = "Salgadinho";
            //    novoProduto.Categoria = "Alimento";
            //    novoProduto.Preco = 5.99;
            //    contexto.Produtos.Add(novoProduto);
            //    ExibirEntries(entries);
            //    var entry = contexto.Entry(novoProduto);
            //    contexto.Produtos.Remove(novoProduto);
            //    Console.WriteLine("\n\n"+entry.Entity.ToString() + " - " + entry.State);



            //    //contexto.SaveChanges();
            //    ExibirEntries(entries);
            //    //Exibir(produtos);


            //}
            #endregion

            #region um para muitos
            // Compra de 6 pães franceses

            //Produto pao = new Produto()
            //{
            //    Nome = "Pão Frances",
            //    Categoria = "Padaria",
            //    PrecoUnitario = 0.40,
            //    Unidade = "Unidade"
            //};

            //Compra compra = new Compra();
            //compra.Quantidade = 6;
            //compra.Produto = pao;
            //compra.Preco = compra.Quantidade * pao.PrecoUnitario;
            //using (var contexto = new LojaContext())
            //{
            //    var entries = contexto.ChangeTracker.Entries();
            //    var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
            //    var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            //    loggerFactory.AddProvider(SqlLoggerProvider.Create());

            //    //contexto.Produtos.Add(pao);
            //    contexto.Compras.Add(compra);
            //    ExibirEntries(entries);
            //    contexto.SaveChanges();
            //}
            #endregion

            #region Muitos para Muitos
            //var promocaoDePascoa = new Promocao();
            //promocaoDePascoa.Descricao = "Páscoa feliz";
            //promocaoDePascoa.DataInicio = DateTime.Now;
            //promocaoDePascoa.DataFim = DateTime.Now.AddMonths(3);

            //Produto p1 = new Produto();
            //p1.Nome = "Suco de Laranja";
            //p1.Categoria = "Bebidas";
            //p1.PrecoUnitario = 8.79;
            //p1.Unidade = "Litros";
            //Produto p2 = new Produto() { Nome = "Café", Categoria = "Bebidas", PrecoUnitario = 12.45, Unidade = "Gramas" };
            //Produto p3 = new Produto() { Nome = "Macarrão", Categoria = "Alimentos", PrecoUnitario = 4.23, Unidade = "Gramas" };



            ////promocaoDePascoa.IncluiProduto(p1);
            ////promocaoDePascoa.IncluiProduto(p2);
            ////promocaoDePascoa.IncluiProduto(p3);

            //using (var contexto = new LojaContext())
            //{
            //    var entries = contexto.ChangeTracker.Entries();

            //    var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
            //    var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            //    loggerFactory.AddProvider(SqlLoggerProvider.Create());

            //    //contexto.Promocao.Add(promocaoDePascoa);
            //    ExibirEntries(entries);

            //    //var promocao = contexto.Promocao.Find(1);
            //    //contexto.Promocao.Remove(promocao);
            //    //ExibirEntries(entries);
            //    //contexto.SaveChanges();
            //    //ExibirEntries(entries);
            #endregion

            #region Um para UM

            //var cliente = new Cliente();
            //cliente.Nome = "Cliente 1";
            //cliente.EnderecoDeEntrega = new Endereco()
            //{
            //    Numero = 0,
            //    Logradouro = "Rua dos bobos",
            //    Complemento = "Casa",
            //    Bairro = "Centro",
            //    Cidade = "São Paulo"
            //};

            //using (var contexto = new LojaContext())
            //{
            //    var entries = contexto.ChangeTracker.Entries();

            //    var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
            //    var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            //    loggerFactory.AddProvider(SqlLoggerProvider.Create());

            //    contexto.Clientes.Add(cliente);

            //    ExibirEntries(entries);

            //    contexto.SaveChanges();


            #endregion


            //ExibePromocao();

            using (var contexto = new LojaContext())
            {
                var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(SqlLoggerProvider.Create());

                var entries = contexto.ChangeTracker.Entries();

                var cliente = contexto.Clientes.Include(e => e.EnderecoDeEntrega).FirstOrDefault();

                Console.WriteLine($"Endereço de entrega - {cliente.EnderecoDeEntrega}");

                //var produto = contexto.Produtos.Include(p => p.Compras).Where(p => p.Id == 1).FirstOrDefault();
                var produto = contexto.Produtos.Where(p => p.Id == 1).FirstOrDefault();
                contexto.Entry(produto).Collection(c => c.Compras).Query().Where(p => p.Preco > 2).Load();

                Console.WriteLine("\nCompras\n");
                foreach (var compras in produto.Compras)
                {
                    Console.WriteLine(compras);
                }

                //foreach (var item in precoMaiorQue)
                //{
                //    Console.WriteLine(item);
                //}
            }


            Console.ReadKey();
        }

        private static void ExibePromocao()
        {
            using (var contexto = new LojaContext())
            {
                var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(SqlLoggerProvider.Create());

                var entries = contexto.ChangeTracker.Entries();
                //IncluirPromocao(contexto, entries);
            }

            using (var contexto2 = new LojaContext())
            {
                var promocao = contexto2.Promocao.Include(p => p.Produtos).ThenInclude(pp => pp.Produto).FirstOrDefault();

                Console.WriteLine("\nMotrando os produtos da promoção...");

                foreach (var item in promocao.Produtos)
                {

                    Console.WriteLine(item.Produto);
                }
            }
        }

        private static void IncluirPromocao(LojaContext contexto, IEnumerable<EntityEntry> entries)
        {
            var promocao = new Promocao();
            promocao.Descricao = "Queima Total Janeiro 2022";
            promocao.DataInicio = new DateTime(2022, 1, 1);
            promocao.DataFim = new DateTime(2022, 1, 31);

            var produtos = contexto.Produtos.Where(p => p.Categoria == "Bebidas").ToList();

            foreach (var item in produtos)
            {
                promocao.IncluiProduto(item);
            }

            contexto.Promocao.Add(promocao);
            ExibirEntries(entries);
            contexto.SaveChanges();
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
