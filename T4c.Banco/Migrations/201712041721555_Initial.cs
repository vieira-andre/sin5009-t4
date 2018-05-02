namespace T4c.Banco.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PedidoFinanciamentoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Cpf = c.String(nullable: false),
                        RendaMensal = c.Double(nullable: false),
                        ValorFinanciamento = c.Double(nullable: false),
                        IsSaudeFinanceiraOk = c.Boolean(nullable: false),
                        AreRendimentosOk = c.Boolean(nullable: false),
                        IsFinanciamentoAprovado = c.Boolean(nullable: false),
                        IsDesembolsoRealizado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PedidoFinanciamentoes");
        }
    }
}
