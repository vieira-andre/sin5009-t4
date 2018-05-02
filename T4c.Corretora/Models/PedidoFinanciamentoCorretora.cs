using System.ComponentModel.DataAnnotations;

namespace T4c.Corretora.Models
{
    public class PedidoFinanciamentoCorretora
    {
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

        [Display(Name = "Status do financiamento")]
        public string StatusFinanciamento { get; set; }

        [Display(Name = "Encaminhado para banco após rejeição")]
        public bool IsRejeitadoEncaminhadoParaBanco { get; set; }

        [Display(Name = "Encaminhado para banco após aprovação?")]
        public bool IsAprovadoEncaminhadoParaBanco { get; set; }

        public void VerificaLimite()
        {
            if (ValorFinanciamento <= 10000)
            {
                IsRejeitadoEncaminhadoParaBanco = false;
            }
            else
            {
                IsRejeitadoEncaminhadoParaBanco = true;
            }

            if (!IsRejeitadoEncaminhadoParaBanco) StatusFinanciamento = "Aprovado! Dentro do limite";
            else StatusFinanciamento = "Negado! Fora do limite";
        }
    }
}