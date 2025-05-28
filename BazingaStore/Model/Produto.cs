using BazingaStore.Model;

public class Produto
{
    public Guid ProdutoId { get; set; } // deixa o banco gerar
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal Preco { get; set; }
    public string? Imagem { get; set; }
    public int Estoque { get; set; }

    public Guid CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }
}
