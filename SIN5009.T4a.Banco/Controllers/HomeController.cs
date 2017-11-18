using SIN5009.T4a.Banco.DAO;
using System.Web.Mvc;

namespace SIN5009.T4a.Banco.Controllers
{
    public class HomeController : Controller
    {
        private PedidoFinanciamentoDAO dao = new PedidoFinanciamentoDAO();

        public ActionResult Index()
        {
            ViewBag.Title = "SIN 5009 - Banco WebAPI";

            return View();
        }

        public ActionResult NovoPedidoFinanciamento(string nome, string cpf, double? rendaMensal, double? valorFinanciamento)
        {
            dao.CriaPedidoFinanciamento(nome, cpf, rendaMensal, valorFinanciamento);

            return View();
        }
    }
}
