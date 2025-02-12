namespace BazingaStore.Model
{
    public class Pedido
    {
        public Guid PedidoId { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid ProdutoId { get; set; }
        public DateTime DataPedido { get; set; }
        public enum StatusPedido
        {
            Pendente,
            Enviado,
            Entregue,
            Cancelado
        }
        public decimal ValorTotal { get; set; }

    }
}
