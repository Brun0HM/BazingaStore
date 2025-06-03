using Microsoft.AspNetCore.Identity;

namespace BazingaStore.Model
{
    public class Pedido
    {


        public Guid PedidoId { get; set; }

        // Relacionamentos
        public Guid? UserId { get; set; } // ID do usuário (Identity)
        public IdentityUser? User { get; set; }

        public Guid ProdutoId { get; set; }

        public Produto? Produto { get; set; }

        public Guid? CupomDescontoId { get; set; } // Nullable pois pode ser sem cupom
        public CupomDesconto? Cupom { get; set; }

        // Propriedades calculadas
        public decimal? ValorOriginal { get; set; }
        public decimal? ValorDesconto { get; set; }
        public decimal? ValorTotal { get; set; }

        public DateTime? DataPedido { get; set; }
    }
}

