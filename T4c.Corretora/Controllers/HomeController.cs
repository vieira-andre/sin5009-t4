using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using T4c.Banco.Models;
using T4c.Corretora.Models;

namespace T4c.Corretora.Controllers
{
    public class HomeController : Controller
    {
        private HttpClient client = new HttpClient() { BaseAddress = new Uri("http://corretora-t4c-sin5009.azurewebsites.net/") };

        public ActionResult Index()
        {
            ViewBag.Title = "Corretora Web App/API";

            return View();
        }

        [System.Web.Http.HttpPost]
        public ActionResult NovoPedidoFinanciamento(PedidoFinanciamentoCorretora pedidoFinanciamentoCorretora)
        {
            var postTask = client.PostAsJsonAsync("api/pedidofinanciamentocorretoras", pedidoFinanciamentoCorretora);
            postTask.Wait();

            var result = postTask.Result;

            if (result.IsSuccessStatusCode)
            {
                int id = GetIdByCpf(pedidoFinanciamentoCorretora.Cpf);

                VerificaLimite(id);

                return RedirectToAction("StatusPedidoFinanciamento", GetPedidoFinanciamentoById(id));
            }

            return View();
        }

        public ActionResult StatusPedidoFinanciamento(PedidoFinanciamentoCorretora pedidoFinanciamentoCorretora)
        {
            return View(pedidoFinanciamentoCorretora);
        }

        public ActionResult SeguirParaBanco(PedidoFinanciamentoCorretora pedidoFinanciamentoCorretora)
        {
            var pedido = new PedidoFinanciamento()
            {
                Nome = pedidoFinanciamentoCorretora.Nome,
                Cpf = pedidoFinanciamentoCorretora.Cpf,
                RendaMensal = pedidoFinanciamentoCorretora.RendaMensal,
                ValorFinanciamento = pedidoFinanciamentoCorretora.ValorFinanciamento
            };

            return View("http://banco-t4c-sin5009.azurewebsites.net/Home/NovoPedidoFinanciamento/", pedido);
        }

        [HttpPut]
        private void VerificaLimite(int id)
        {
            var pedidoFinanciamentoCorretora = GetPedidoFinanciamentoById(id);

            var putTask = client.PutAsJsonAsync("api/pedidofinanciamentocorretoras/verificaLimite?id=" + id.ToString(), pedidoFinanciamentoCorretora);
            putTask.Wait();
        }

        [HttpGet]
        private PedidoFinanciamentoCorretora GetPedidoFinanciamentoById(int id)
        {
            var getTask = client.GetAsync("api/pedidofinanciamentocorretoras/" + id.ToString());
            getTask.Wait();

            var result = getTask.Result;

            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<PedidoFinanciamentoCorretora>();
                readTask.Wait();

                return readTask.Result;
            }

            ModelState.AddModelError(string.Empty, "Erro no procedimento");

            return null;
        }

        [HttpGet]
        private int GetIdByCpf(string cpf)
        {
            var getTask = client.GetAsync("api/pedidofinanciamentocorretoras/byCpf?cpf=" + cpf);
            getTask.Wait();

            var result = getTask.Result;

            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<PedidoFinanciamentoCorretora>>();
                readTask.Wait();

                return readTask.Result.FirstOrDefault().Id;
            }

            ModelState.AddModelError(string.Empty, "Erro no procedimento");

            return -1;
        }
    }
}
