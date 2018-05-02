namespace T4c.Banco.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StatusFinanciamento : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PedidoFinanciamentoes", "StatusFinanciamento", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PedidoFinanciamentoes", "StatusFinanciamento");
        }
    }
}
