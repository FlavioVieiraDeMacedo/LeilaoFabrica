using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fiap.Leilao.Web.Controllers
{
    public class UsuarioController : Controller
    {

        #region GETS
        [HttpGet]
        public ActionResult Cadastrar()
        {
            return View();
        }

        #endregion

        #region POSTS
        [HttpPost]
        public ActionResult Cadastrar()
        {
            return View();
        }
        #endregion
    }
}