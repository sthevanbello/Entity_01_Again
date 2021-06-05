using System.Globalization;

namespace Alura.Loja.Testes.ConsoleApp
{
    public class Compra
    {
        public Compra()
        {
        }

        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; internal set; }
        public int Quantidade { get; internal set; }
        public double Preco { get; internal set; }

        public override string ToString()
        {
            return $"Id: {ProdutoId} - Nome: {Produto.Nome} - Quantidade: {Quantidade} - Preço: {Preco.ToString("C", CultureInfo.CurrentCulture)}";
        }
    }
}