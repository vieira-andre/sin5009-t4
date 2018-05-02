using System.Data.Entity;

namespace T4c.Banco.Models
{
    public class Context : DbContext
    {
        public Context() : base("name=Context")
        {
        }

        public DbSet<PedidoFinanciamento> PedidosFinanciamento { get; set; }
    }
}