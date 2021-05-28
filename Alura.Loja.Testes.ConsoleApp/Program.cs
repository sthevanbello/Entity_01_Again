﻿using System;
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
            //GravarUsandoAdoNet();
            //GravarUsandoEntity();
            //RecuperarProdutos();
            //ExcluirProdutos();
            RecuperarProdutos();
            AtualizarProduto();

            Console.ReadKey();
        }

        private static void AtualizarProduto()
        {
            //GravarUsandoEntity();
            RecuperarProdutos();

            using (var repo = new ProdutoDAOEntity())
            {
                var produto = repo.Produtos();
                foreach (var item in produto)
                {

                    item.Nome = "Harry Potter - Ordem da Fênix - Editado 2";
                    repo.Atualizar(item);
                }

            }

        }

        private static void ExcluirProdutos()
        {
            using (var repo = new ProdutoDAOEntity())
            {
                IList<Produto> produtos = repo.Produtos();
                foreach (var produto in produtos)
                {
                    repo.Remover(produto);
                }

            }
        }

        private static void RecuperarProdutos()
        {
            using (var repo = new ProdutoDAOEntity())
            {
                IList<Produto> produtos = repo.Produtos();
                Console.WriteLine($"Foram encontrado(s) {produtos.Count} produtos\n");
                foreach (var item in produtos)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine("\nFim da lista");

            }
        }

        private static void GravarUsandoEntity()
        {
            Produto p = new Produto();
            p.Nome = "Harry Potter e a Ordem da Fênix";
            p.Categoria = "Livros";
            p.Preco = 19.89;

            using (var contexto = new ProdutoDAOEntity())
            {
                contexto.Adicionar(p);

            }
        }

        private static void GravarUsandoAdoNet()
        {
            Produto p = new Produto();
            p.Nome = "Harry Potter e a Ordem da Fênix";
            p.Categoria = "Livros";
            p.Preco = 19.89;

            using (var repo = new ProdutoDAO())
            {
                repo.Adicionar(p);
            }
        }
    }
}
