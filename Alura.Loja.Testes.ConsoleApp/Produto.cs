namespace Alura.Loja.Testes.ConsoleApp
{
    public class Produto
    {
        public int Id { get; internal set; }
        public string Nome { get; internal set; }
        public string Categoria { get; internal set; }
        public double PrecoUnitario { get; internal set; }
        public string Unidade { get; set; }



        public override string ToString()
        {
            return $"{Id} - {Nome.PadRight(40)}{Categoria.PadRight(20)}{PrecoUnitario}";
        }
    }
}