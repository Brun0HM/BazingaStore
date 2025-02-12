using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BazingaStore.Model;

namespace BazingaStore.Data
{
    public class ApiDbContext : IdentityDbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }
        public DbSet<BazingaStore.Model.Produto> Produto { get; set; } = default!;
        public DbSet<BazingaStore.Model.Categoria> Categoria { get; set; } = default!;
        public DbSet<BazingaStore.Model.CupomDesconto> CupomDesconto { get; set; } = default!;
        public DbSet<BazingaStore.Model.ItemVenda> ItemVenda { get; set; } = default!;
    }
}
