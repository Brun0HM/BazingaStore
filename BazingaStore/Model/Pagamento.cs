namespace BazingaStore.Model
{
    public class Pagamento
    {
        public Guid PagamentoId { get; set; }
        public Guid PedidoId { get; set; }
        public enum MetodoPagamento
        {
            CartaoCredito,
            CaertaoDebito,
            Boleto,
            Pix
        }
        public enum StatusPagamento
        {
            Aprovado,
            aguardando,
            Pago,
            Recusado
        }
        public DateTime DataPagamento { get; set; }
    }
}
