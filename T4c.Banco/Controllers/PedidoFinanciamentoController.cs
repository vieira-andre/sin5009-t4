using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using T4c.Banco.Models;

namespace T4c.Banco.Controllers
{
    [RoutePrefix("api/pedidofinanciamento")]
    public class PedidoFinanciamentoController : ApiController
    {
        private Context db = new Context();

        [HttpGet]
        public IQueryable<PedidoFinanciamento> GetPedidosFinanciamento()
        {
            return db.PedidosFinanciamento;
        }

        [HttpGet]
        [ResponseType(typeof(PedidoFinanciamento))]
        public IHttpActionResult GetPedidoFinanciamento(int id)
        {
            var pedidoFinanciamento = db.PedidosFinanciamento.Find(id);

            if (pedidoFinanciamento == null)
            {
                return NotFound();
            }

            return Ok(pedidoFinanciamento);
        }

        [HttpGet]
        [Route("byCpf")]
        [ResponseType(typeof(PedidoFinanciamento))]
        public IHttpActionResult GetPedidoFinanciamentoByCpf(string cpf)
        {
            var pedidoFinanciamento = db.PedidosFinanciamento.Where(p => p.Cpf == cpf);

            if (cpf == null)
            {
                return NotFound();
            }

            return Ok(pedidoFinanciamento);
        }

        [HttpPut]
        [Route("verificaSaudeFinanceira")]
        [ResponseType(typeof(void))]
        public IHttpActionResult VerificaSaudeFinanceira(int id)
        {
            var pedidoFinanciamento = db.PedidosFinanciamento.Find(id);
            pedidoFinanciamento.VerificaSaudeFinanceira();

            PutPedidoFinanciamento(pedidoFinanciamento.Id, pedidoFinanciamento);

            return Ok();
        }

        [HttpPut]
        [Route("verificaRendimentos")]
        [ResponseType(typeof(void))]
        public IHttpActionResult VerificaRendimentosComprovados(int id)
        {
            var pedidoFinanciamento = db.PedidosFinanciamento.Find(id);
            pedidoFinanciamento.VerificaRendimentos();

            PutPedidoFinanciamento(pedidoFinanciamento.Id, pedidoFinanciamento);

            return Ok();
        }

        [HttpPut]
        [Route("verificaViabilidade")]
        [ResponseType(typeof(void))]
        public IHttpActionResult VerificaViabilidadeFinanciamento(int id)
        {
            var pedidoFinanciamento = db.PedidosFinanciamento.Find(id);
            pedidoFinanciamento.VerificaViabilidadeFinanciamento();

            PutPedidoFinanciamento(pedidoFinanciamento.Id, pedidoFinanciamento);

            return Ok();
        }

        [HttpPut]
        [Route("trataDesembolso")]
        [ResponseType(typeof(void))]
        public IHttpActionResult TrataDesembolso(int id)
        {
            var pedidoFinanciamento = db.PedidosFinanciamento.Find(id);
            pedidoFinanciamento.TrataDesembolso();

            PutPedidoFinanciamento(pedidoFinanciamento.Id, pedidoFinanciamento);

            return Ok();
        }

        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPedidoFinanciamento(int id, PedidoFinanciamento pedidoFinanciamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pedidoFinanciamento.Id)
            {
                return BadRequest();
            }

            db.Entry(pedidoFinanciamento).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoFinanciamentoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [ResponseType(typeof(PedidoFinanciamento))]
        public IHttpActionResult PostPedidoFinanciamento(PedidoFinanciamento pedidoFinanciamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PedidosFinanciamento.Add(pedidoFinanciamento);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pedidoFinanciamento.Id }, pedidoFinanciamento);
        }

        [HttpDelete]
        [ResponseType(typeof(PedidoFinanciamento))]
        public IHttpActionResult DeletePedidoFinanciamentoById(int id)
        {
            var pedidoFinanciamento = db.PedidosFinanciamento.Find(id);

            if (pedidoFinanciamento == null)
            {
                return NotFound();
            }

            db.PedidosFinanciamento.Remove(pedidoFinanciamento);
            db.SaveChanges();

            return Ok(pedidoFinanciamento);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }

        private bool PedidoFinanciamentoExists(int id)
        {
            return db.PedidosFinanciamento.Count(e => e.Id == id) > 0;
        }
    }
}