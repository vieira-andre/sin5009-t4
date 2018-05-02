using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace T4c.Corretora.Models
{
    public class CorretoraContext : DbContext
    {
        public CorretoraContext() : base("CorretoraContext")
        {
        }

        public DbSet<PedidoFinanciamentoCorretora> PedidoFinanciamentoCorretoras { get; set; }
    }
}
