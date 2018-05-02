using System;
using System.ComponentModel.DataAnnotations;

namespace T4c.Banco.Models
{
    public class PedidoFinanciamento
    {
        private bool _isSaudeFinanceiraOk;
        private bool _areRendimentosOk;

        public int Id { get; set; }

        [Required]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "CPF")]
        public string Cpf { get; set; }

        [Required]
        [Display(Name = "Renda mensal")]
        public double RendaMensal { get; set; }

        [Required]
        [Display(Name = "Valor solicitado de financiamento")]
        public double ValorFinanciamento { get; set; }

        [Display(Name = "Apresenta saúde financeira?")]
        public bool IsSaudeFinanceiraOk { get { return _isSaudeFinanceiraOk; } set { _isSaudeFinanceiraOk = value; } }

        [Display(Name = "Rendimentos comprovados em ordem?")]
        public bool AreRendimentosOk { get { return _areRendimentosOk; } set { _areRendimentosOk = value; } }

        public bool IsFinanciamentoAprovado { get; set; }

        [Display(Name = "Status do financiamento")]
        public string StatusFinanciamento { get; set; }

        [Display(Name = "Proposta escolhida")]
        public string PropostaEscolhida { get; set; }

        [Display(Name = "Pedido proveniente da corretora")]
        public bool FromCorretora { get; set; }

        public bool IsDesembolsoRealizado { get; set; }

        public void VerificaSaudeFinanceira()
        {
            IsSaudeFinanceiraOk = new Random().NextDouble() <= 0.75;
        }

        public void VerificaRendimentos()
        {
            AreRendimentosOk = new Random().NextDouble() <= 0.75;
        }

        public void VerificaViabilidadeFinanciamento()
        {
            if (IsSaudeFinanceiraOk && AreRendimentosOk)
            {
                if ((RendaMensal >= 2500 && RendaMensal <= 3500) && ValorFinanciamento <= 15000)
                {
                    IsFinanciamentoAprovado = true;
                }
                else if ((RendaMensal > 3500 && RendaMensal <= 5000) && ValorFinanciamento <= 30000)
                {
                    IsFinanciamentoAprovado = true;
                }
                else if ((RendaMensal > 5000 && RendaMensal <= 10000) && ValorFinanciamento <= 60000)
                {
                    IsFinanciamentoAprovado = true;
                }
                else if (RendaMensal > 10000 && ValorFinanciamento <= 150000)
                {
                    IsFinanciamentoAprovado = true;
                }
                else
                {
                    IsFinanciamentoAprovado = false;
                }
            }
            else
            {
                IsFinanciamentoAprovado = false;
            }

            if (IsFinanciamentoAprovado) StatusFinanciamento = "Aprovado!";
            else StatusFinanciamento = "Negado!";
        }

        public void TrataDesembolso()
        {
            IsDesembolsoRealizado = true;
        }
    }
}