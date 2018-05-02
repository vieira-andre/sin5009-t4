namespace T4c.Corretora.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PedidoFinanciamentoCorretoras",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Cpf = c.String(nullable: false),
                        RendaMensal = c.Double(nullable: false),
                        ValorFinanciamento = c.Double(nullable: false),
                        IsFinanciamentoAprovado = c.Boolean(nullable: false),
                        StatusFinanciamento = c.String(),
                        RejeitadoEncaminhadoParaBanco = c.Boolean(nullable: false),
                        AprovadoEncaminhadoParaBanco = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PedidoFinanciamentoCorretoras");
        }
    }
}
