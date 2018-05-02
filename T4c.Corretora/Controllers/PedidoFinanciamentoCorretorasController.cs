using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using T4c.Corretora.Models;

namespace T4c.Corretora.Controllers
{
    [RoutePrefix("api/pedidofinanciamentocorretoras")]
    public class PedidoFinanciamentoCorretorasController : ApiController
    {
        private HttpClient client = new HttpClient() { BaseAddress = new Uri("http://corretora-t4c-sin5009.azurewebsites.com/") };

        private CorretoraContext db = new CorretoraContext();

        [HttpGet]
        public IQueryable<PedidoFinanciamentoCorretora> GetPedidoFinanciamentoCorretoras()
        {
            return db.PedidoFinanciamentoCorretoras;
        }

        [HttpGet]
        [ResponseType(typeof(PedidoFinanciamentoCorretora))]
        public IHttpActionResult GetPedidoFinanciamentoCorretora(int id)
        {
            PedidoFinanciamentoCorretora pedidoFinanciamentoCorretora = db.PedidoFinanciamentoCorretoras.Find(id);
            if (pedidoFinanciamentoCorretora == null)
            {
                return NotFound();
            }

            return Ok(pedidoFinanciamentoCorretora);
        }

        [HttpGet]
        [Route("byCpf")]
        [ResponseType(typeof(PedidoFinanciamentoCorretora))]
        public IHttpActionResult GetPedidoFinanciamentoByCpf(string cpf)
        {
            var pedidoFinanciamentoCorretora = db.PedidoFinanciamentoCorretoras.Where(p => p.Cpf == cpf);

            if (cpf == null)
            {
                return NotFound();
            }

            return Ok(pedidoFinanciamentoCorretora);
        }

        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPedidoFinanciamentoCorretora(int id, PedidoFinanciamentoCorretora pedidoFinanciamentoCorretora)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pedidoFinanciamentoCorretora.Id)
            {
                return BadRequest();
            }

            db.Entry(pedidoFinanciamentoCorretora).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoFinanciamentoCorretoraExists(id))
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
        [ResponseType(typeof(PedidoFinanciamentoCorretora))]
        public IHttpActionResult PostPedidoFinanciamentoCorretora(PedidoFinanciamentoCorretora pedidoFinanciamentoCorretora)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PedidoFinanciamentoCorretoras.Add(pedidoFinanciamentoCorretora);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pedidoFinanciamentoCorretora.Id }, pedidoFinanciamentoCorretora);
        }

        [HttpDelete]
        [ResponseType(typeof(PedidoFinanciamentoCorretora))]
        public IHttpActionResult DeletePedidoFinanciamentoCorretora(int id)
        {
            PedidoFinanciamentoCorretora pedidoFinanciamentoCorretora = db.PedidoFinanciamentoCorretoras.Find(id);
            if (pedidoFinanciamentoCorretora == null)
            {
                return NotFound();
            }

            db.PedidoFinanciamentoCorretoras.Remove(pedidoFinanciamentoCorretora);
            db.SaveChanges();

            return Ok(pedidoFinanciamentoCorretora);
        }

        [HttpPut]
        [Route("verificaLimite")]
        [ResponseType(typeof(void))]
        public IHttpActionResult VerificaLimite(int id)
        {
            var pedidoFinanciamentoCorretora = db.PedidoFinanciamentoCorretoras.Find(id);
            pedidoFinanciamentoCorretora.VerificaLimite();

            PutPedidoFinanciamentoCorretora(pedidoFinanciamentoCorretora.Id, pedidoFinanciamentoCorretora);

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PedidoFinanciamentoCorretoraExists(int id)
        {
            return db.PedidoFinanciamentoCorretoras.Count(e => e.Id == id) > 0;
        }
    }
}