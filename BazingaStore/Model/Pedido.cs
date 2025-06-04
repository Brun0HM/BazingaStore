using Microsoft.AspNetCore.Identity;

namespace BazingaStore.Model
{
    public class Pedido
    {


        public Guid PedidoId { get; set; }

        // Relacionamentos
        public Guid? UserId { get; set; } // ID do usuário (Identity)
        public IdentityUser? User { get; set; }

        public DateTime? DataPedido { get; set; }
    }
}

