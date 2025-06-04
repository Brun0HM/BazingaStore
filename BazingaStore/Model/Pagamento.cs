namespace BazingaStore.Model
{
    public class Pagamento
    {
        public Guid PagamentoId { get; set; }
        public Guid PedidoId { get; set; }
        public Guid CarrinhoId { get; set; }
        public enum MetodoPagamento
        {
            CartaoCredito,
            CaertaoDebito,
            Boleto,
            Pix
        }

        public DateTime DataPagamento { get; set; }
    }
}
