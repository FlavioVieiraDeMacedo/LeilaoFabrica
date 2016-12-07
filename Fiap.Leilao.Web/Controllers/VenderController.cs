using Fiap.Leilao.Web.UnitsOfWork;
using Fiap.Leilao.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fiap.Leilao.Web.Controllers
{
    public class VenderController : Controller
    {
        #region FIELDs
        private UnitOfWork _unit = new UnitOfWork();
        #endregion

        #region GETs
        [HttpGet]
        public ActionResult Vender()
        {
            return View();
        }

        #endregion

        #region POSTs

        [HttpPost]
        public ActionResult Vender(VendaViewModel vViewModel)
        {
            return View();
        }

        #endregion
    }
}