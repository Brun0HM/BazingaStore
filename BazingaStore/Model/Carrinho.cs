namespace BazingaStore.Model
{
    public class Carrinho
    {
        public Guid CarrinhoId { get; set; }
        public Guid UsuarioId { get; set; }
        public List<CarrinhoItem>? Itens { get; set; } = new List<CarrinhoItem>();
        public decimal? Total { get; set; } = 0;
    }
}
