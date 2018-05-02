namespace T4c.Corretora.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ajust : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PedidoFinanciamentoCorretoras", "IsRejeitadoEncaminhadoParaBanco", c => c.Boolean(nullable: false));
            AddColumn("dbo.PedidoFinanciamentoCorretoras", "IsAprovadoEncaminhadoParaBanco", c => c.Boolean(nullable: false));
            DropColumn("dbo.PedidoFinanciamentoCorretoras", "IsFinanciamentoAprovado");
            DropColumn("dbo.PedidoFinanciamentoCorretoras", "RejeitadoEncaminhadoParaBanco");
            DropColumn("dbo.PedidoFinanciamentoCorretoras", "AprovadoEncaminhadoParaBanco");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PedidoFinanciamentoCorretoras", "AprovadoEncaminhadoParaBanco", c => c.Boolean(nullable: false));
            AddColumn("dbo.PedidoFinanciamentoCorretoras", "RejeitadoEncaminhadoParaBanco", c => c.Boolean(nullable: false));
            AddColumn("dbo.PedidoFinanciamentoCorretoras", "IsFinanciamentoAprovado", c => c.Boolean(nullable: false));
            DropColumn("dbo.PedidoFinanciamentoCorretoras", "IsAprovadoEncaminhadoParaBanco");
            DropColumn("dbo.PedidoFinanciamentoCorretoras", "IsRejeitadoEncaminhadoParaBanco");
        }
    }
}
