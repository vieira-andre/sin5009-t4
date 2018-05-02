using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using T4c.Banco.Models;

namespace T4c.Banco.Controllers
{
    public class HomeController : Controller
    {
        private HttpClient client = new HttpClient() { BaseAddress = new Uri("http://banco-t4c-sin5009.azurewebsites.net/") };

        public ActionResult Index()
        {
            ViewBag.Title = "Banco Web App/API";

            return View();
        }

        [System.Web.Http.HttpPost]
        public ActionResult NovoPedidoFinanciamento(PedidoFinanciamento pedidoFinanciamento)
        {
            var postTask = client.PostAsJsonAsync("api/pedidofinanciamento", pedidoFinanciamento);
            postTask.Wait();

            var result = postTask.Result;

            if (result.IsSuccessStatusCode)
            {
                int id = GetIdByCpf(pedidoFinanciamento.Cpf);

                VerificaSituacoesCliente(id);
                VerificaViabilidadeFinanciamento(id);

                return RedirectToAction("StatusPedidoFinanciamento", GetPedidoFinanciamentoById(id));
            }

            return View();
        }

        public ActionResult StatusPedidoFinanciamento(PedidoFinanciamento pedidoFinanciamento)
        {
            return View(pedidoFinanciamento);
        }

        public ActionResult AposStatusPedidoFinanciamento(PedidoFinanciamento pedidoFinanciamento)
        {
            if (!pedidoFinanciamento.IsFinanciamentoAprovado)
            {
                return RedirectToAction("PedidoNegado", pedidoFinanciamento);
            }

            var putTask = client.PutAsJsonAsync("api/pedidofinanciamento/trataDesembolso?id=" + pedidoFinanciamento.Id.ToString(), pedidoFinanciamento);
            putTask.Wait();

            return RedirectToAction("FinalizacaoPedido", pedidoFinanciamento);
        }

        public ActionResult PedidoNegado(PedidoFinanciamento pedidoFinanciamento)
        {
            return View(pedidoFinanciamento);
        }

        public ActionResult FinalizacaoPedido(PedidoFinanciamento pedidoFinanciamento)
        {
            return View(pedidoFinanciamento);
        }

        [HttpPut]
        private void VerificaSituacoesCliente(int id)
        {
            var pedidoFinanciamento = GetPedidoFinanciamentoById(id);

            var v1Task = client.PutAsJsonAsync("api/pedidofinanciamento/verificaSaudeFinanceira?id=" + id.ToString(), pedidoFinanciamento);
            v1Task.Wait();

            var v2Task = client.PutAsJsonAsync("api/pedidofinanciamento/verificaRendimentos?id=" + id.ToString(), pedidoFinanciamento);
            v2Task.Wait();
        }

        [HttpPut]
        private void VerificaViabilidadeFinanciamento(int id)
        {
            var pedidoFinanciamento = GetPedidoFinanciamentoById(id);

            var putTask = client.PutAsJsonAsync("api/pedidofinanciamento/verificaViabilidade?id=" + id.ToString(), pedidoFinanciamento);
            putTask.Wait();
        }

        [HttpGet]
        private PedidoFinanciamento GetPedidoFinanciamentoById(int id)
        {
            var getTask = client.GetAsync("api/pedidofinanciamento/" + id.ToString());
            getTask.Wait();

            var result = getTask.Result;

            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<PedidoFinanciamento>();
                readTask.Wait();

                return readTask.Result;
            }

            ModelState.AddModelError(string.Empty, "Erro no procedimento");

            return null;
        }

        [HttpGet]
        private int GetIdByCpf(string cpf)
        {
            var getTask = client.GetAsync("api/pedidofinanciamento/byCpf?cpf=" + cpf);
            getTask.Wait();

            var result = getTask.Result;

            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<PedidoFinanciamento>>();
                readTask.Wait();

                return readTask.Result.FirstOrDefault().Id;
            }

            ModelState.AddModelError(string.Empty, "Erro no procedimento");

            return -1;
        }
    }
}
