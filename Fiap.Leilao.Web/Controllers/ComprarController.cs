using Fiap.Leilao.Web.UnitsOfWork;
using Fiap.Leilao.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fiap.Leilao.Web.Controllers
{
    public class ComprarController : Controller
    {
        private UnitOfWork _unit = new UnitOfWork();
        // GET: Comprar
        public ActionResult Dashboard()
        {

            var compras = new CompraViewModel()
            {
                Negociacoes = _unit.NegociacaoRepository.BuscarPor(n => n.Id_Comprador == null)

            };
            return View(compras);

        }
    }
}