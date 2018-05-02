namespace T4c.Corretora.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<T4c.Corretora.Models.CorretoraContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(T4c.Corretora.Models.CorretoraContext context)
        {
            
        }
    }
}
