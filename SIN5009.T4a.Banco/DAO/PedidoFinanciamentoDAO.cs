using SIN5009.T4a.Banco.Models;
using System.Collections.Generic;

namespace SIN5009.T4a.Banco.DAO
{
    public class PedidoFinanciamentoDAO
    {
        private static List<PedidoFinanciamento> pedidosFinanciamento = new List<PedidoFinanciamento>();
        private static string[] propostas;

        public List<PedidoFinanciamento> GetPedidosFinanciamento()
        {
            return pedidosFinanciamento;
        }

        public void CriaPedidoFinanciamento(string nome, string cpf, double? rendaMensal, double? valorFinanciamento)
        {
            pedidosFinanciamento.Add(new PedidoFinanciamento(nome, cpf, rendaMensal, valorFinanciamento));
        }

        public void InicializaPropostas()
        {
            propostas = new string[3] { "Proposta #1", "Proposta #2", "Proposta #3" };
        }

        public string[] GetPropostas()
        {
            return propostas;
        }
    }
}