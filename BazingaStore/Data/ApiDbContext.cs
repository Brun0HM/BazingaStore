using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BazingaStore.Model;
using BazingaStore.Data;

namespace BazingaStore.Data
{
    public class ApiDbContext : IdentityDbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }
        public DbSet<BazingaStore.Model.Produto> Produto { get; set; } = default!;
        public DbSet<BazingaStore.Model.Categoria> Categoria { get; set; } = default!;
        public DbSet<BazingaStore.Model.Venda> Venda { get; set; } = default!;
        public DbSet<BazingaStore.Data.Pagamento> Pagamento { get; set; } = default!;
        public DbSet<BazingaStore.Model.Pedido> Pedido { get; set; } = default!;
    }
}
