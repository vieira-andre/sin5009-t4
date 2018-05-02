namespace T4c.Banco.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTwoAttributes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PedidoFinanciamentoes", "PropostaEscolhida", c => c.String());
            AddColumn("dbo.PedidoFinanciamentoes", "FromCorretora", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PedidoFinanciamentoes", "FromCorretora");
            DropColumn("dbo.PedidoFinanciamentoes", "PropostaEscolhida");
        }
    }
}
