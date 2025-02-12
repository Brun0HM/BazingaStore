namespace BazingaStore.Model
{
    public class CarrinhoItem
    {
        public Guid CarrinhoItemId { get; set; }
        public Guid CarrinhoId { get; set; }
        public Guid ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
    }
}
