using Microsoft.EntityFrameworkCore;
using Sistema_de_Gestão_de_Estoque.Models;

namespace Sistema_de_Gestão_de_Estoque.data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options) { }
            public DbSet<Fornecedor> Fornecedores { get; set; }
            public DbSet<Produto> Produtos { get; set; }
            public DbSet<Usuario> Usuarios { get; set; }

    }
}
