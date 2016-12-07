using Fiap.Leilao.Web.Models;
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
            var viewModel = new VendaViewModel()
            {
                Produtos = ListarProdutos()
            };
            return View(viewModel);
        }

        #endregion

        #region POSTs

        [HttpPost]
        public ActionResult Vender(VendaViewModel vViewModel)
        {            
            //popular tabela negociacao
            if (ModelState.IsValid)
            {
                var negociacao = new Negociacao()
                {
                    Id_Vendedor = vViewModel.VendedorId,
                    Id_Produto = vViewModel.ProdutoId,
                    Valor_Produto = vViewModel.ValorProduto,
                    Valor_Vendedor = vViewModel.ValorVendedor,
                    Status = vViewModel.Status
                };
                               

                return View();
            }else
            {
                return View();
            }
            
        }

        #endregion

        #region PRIVATE
        public ICollection<Produto> ListarProdutos()
        {
            return _unit.ProdutoRepository.Listar();
        }
        #endregion
    }
}