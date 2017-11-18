using System;

namespace SIN5009.T4a.Banco.Models
{
	public class PedidoFinanciamento
	{
		private bool _isSaudeFinanceiraOk;
		private bool _areRendimentosOk;

		public string Nome { get; private set; }
		public string Cpf { get; private set; }
		public double? RendaMensal { get; private set; }
		public double? ValorFinanciamento { get; private set; }
		public bool IsSaudeFinanceiraOk { get { return _isSaudeFinanceiraOk; } set { _isSaudeFinanceiraOk = value; } }
		public bool AreRendimentosOk { get { return _areRendimentosOk; } set { _areRendimentosOk = value; } }
		public bool IsFinanciamentoAprovado { get; private set; }
		public string StatusFinanciamento { get; private set; }
		public bool IsDesembolsoRealizado { get; private set; }

		public PedidoFinanciamento(string nome, string cpf, double? rendaMensal, double? valorFinanciamento)
		{
			Nome = nome;
			Cpf = cpf;
			RendaMensal = rendaMensal;
			ValorFinanciamento = valorFinanciamento;
		}

		public bool VerificaSaudeFinanceira()
		{
			IsSaudeFinanceiraOk = new Random().NextDouble() <= 0.75;

			return IsSaudeFinanceiraOk;
		}

		public bool VerificaRendimentos()
		{
			AreRendimentosOk = new Random().NextDouble() <= 0.75;

			return AreRendimentosOk;
		}

		public bool VerificaViabilidadeFinanciamento()
		{
			if (IsSaudeFinanceiraOk && AreRendimentosOk)
			{
				if (RendaMensal <= 5000)
				{
					if (ValorFinanciamento <= 10000)
					{
						IsFinanciamentoAprovado = true;
					}
				}
				else if (RendaMensal <= 10000)
				{
					if (ValorFinanciamento <= 50000)
					{
						IsFinanciamentoAprovado = true;
					}
				}
				else
				{
					if (ValorFinanciamento <= 200000)
					{
						IsFinanciamentoAprovado = true;
					}
				}
			}

			if (IsFinanciamentoAprovado)
			{
				StatusFinanciamento = "Aprovado!";
			}
			else
			{
				StatusFinanciamento = "Negado!";
			};

			return IsFinanciamentoAprovado;
		}
	}
}