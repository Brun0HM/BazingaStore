namespace BazingaStore.Data
{
    public class Pagamento
    {
        public Guid PagamentoId { get; set; }

        public Guid PedidoId { get; set; }
        public enum MetodoPagamento
        {
            CartaoCredito,
            CartaoDebito,
            Boleto,
            Pix
        }
        public enum StatusPagamento
        {
            Pendente,
            Aprovado,
            Recusado
        }

        public DateTime DataPagamento { get; set; }

    }
}
