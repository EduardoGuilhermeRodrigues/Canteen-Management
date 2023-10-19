using Microsoft.EntityFrameworkCore;
using TrabalhoCantina.Models;

namespace TrabalhoCantina.Data
{
    public class CantinaDbContext : DbContext
    {
        public CantinaDbContext(DbContextOptions<CantinaDbContext> options) : base(options)
        {
        }

        // DbSet para a entidade Cliente
        public DbSet<Cliente> Clientes { get; set; }

        // DbSet para a entidade Produto
        public DbSet<Produto> Produtos { get; set; }

        // DbSet para a entidade Venda
        public DbSet<Venda> Vendas { get; set; }

        public DbSet<DetalheVenda> DetalheDasVendas { get; set; }

        // Restante das configurações e DbSet para outras entidades, se necessário

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurações do modelo, chaves primárias, relacionamentos, etc.
        }
    }
}
