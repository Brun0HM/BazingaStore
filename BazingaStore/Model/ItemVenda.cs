namespace BazingaStore.Model
{
    public class ItemVenda
    {
        public Guid ItemVendaId { get; set; }
        public Guid VendaId { get; set; }
        public Guid ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }
}
