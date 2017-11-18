using SIN5009.T4a.Banco.DAO;
using SIN5009.T4a.Banco.Models;
using System;
using System.Linq;
using System.Text;
using System.Web.Http;

namespace SIN5009.T4a.Banco.Controllers
{
    public class BancoController : ApiController
	{
        private PedidoFinanciamentoDAO dao = new PedidoFinanciamentoDAO();

        [AcceptVerbs("GET")]
		[Route("getUltimoPedidoFinanciamento")]
		public PedidoFinanciamento GetUltimoPedidoFinanciamento()
		{
			return dao.GetPedidosFinanciamento().LastOrDefault();
		}

        [AcceptVerbs("GET")]
        [Route("getPedidoFinanciamento/{cpf}")]
        public PedidoFinanciamento GetPedidoFinanciamento(string cpf)
        {
            return dao.GetPedidosFinanciamento()
                .Where(c => c.Cpf == cpf)
                .Select(c => c)
                .FirstOrDefault();
        }

        [AcceptVerbs("GET")]
		[Route("verificaSaudeFinanceira/{cpf}")]
		public Object VerificaSaudeFinanceira(string cpf)
		{
            bool isSaudeFinanceiraOk = GetPedidoFinanciamento(cpf).VerificaSaudeFinanceira();

			return new { r = isSaudeFinanceiraOk };
		}

		[AcceptVerbs("GET")]
		[Route("verificaRendimentos/{cpf}")]
		public Object VerificaRendimentos(string cpf)
		{
			bool areRendimentosOk = GetPedidoFinanciamento(cpf).VerificaRendimentos();

			return new { r = areRendimentosOk };
		}

		[AcceptVerbs("GET")]
		[Route("verificaViabilidadeFinanciamento/{cpf}")]
		public Object VerificaViabilidadeFinanciamento(string cpf)
		{
            PedidoFinanciamento pedido = GetPedidoFinanciamento(cpf);

			pedido.VerificaViabilidadeFinanciamento();

			return new { r = pedido.StatusFinanciamento };
		}

		[AcceptVerbs("GET")]
		[Route("calculaPropostas")]
		public void CalculaPropostas()
		{
            dao.InicializaPropostas();
		}

        [AcceptVerbs("GET")]
        [Route("getPropostas")]
        public string GetPropostas()
        {
            StringBuilder strBld = new StringBuilder();

            foreach (string s in dao.GetPropostas())
            {
                strBld.Append(s);
                strBld.Append(";");
            }

            strBld.Remove(strBld.Length - 1, 1);

            return strBld.ToString();
        }

        [AcceptVerbs("GET")]
		[Route("trataDesembolso/{cpf}")]
		public Object TrataDesembolso(string cpf)
		{
			bool isDesembolsoRealizado = GetPedidoFinanciamento(cpf).IsDesembolsoRealizado;
			isDesembolsoRealizado = true;

			return new { r = isDesembolsoRealizado };
		}
	}
}