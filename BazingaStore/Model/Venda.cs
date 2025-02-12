using System.Security.Cryptography.X509Certificates;

namespace BazingaStore.Model
{
    public class Venda
    {
        public Guid VendaId { get; set; }
        public Guid usuarioId { get; set; }
        public DateTime DataVenda { get; set; }
        public enum StatusVenda
        {
            AguardandoPagamento,
            Pago,
            Enviado,
            Entregue,
            Cancelado
        }

        public decimal ValorTotal { get; set; }
    }
}
