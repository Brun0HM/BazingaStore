// Model/Produto.cs
namespace BazingaStore.Model
{
    public class Produto
    {
        public Guid ProdutoId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public string? Imagem { get; set; }
        public int Estoque { get; set; }
        public Guid CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }

        // Propriedade não mapeada para exibir a média das avaliações
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public double? MediaAvaliacao { get; set; }
    }
}
