using System;
using System.Net;
using System.Web.Http;

namespace SIN5009.T4a.Corretora.Controllers
{
    public class CorretoraController : ApiController
    {
        [AcceptVerbs("GET")]
        [Route("apresentaPropostasAoCliente")]
        public Object ApresentaPropostasAoCliente()
        {
            string propostasComoString = new WebClient().DownloadString("http://sin5009t4abanco.azurewebsites.net/getPropostas");

            string[] propostas = propostasComoString.Split(';');

            return new { r = propostas[new Random().Next(propostas.Length)] };
        }
    }
}