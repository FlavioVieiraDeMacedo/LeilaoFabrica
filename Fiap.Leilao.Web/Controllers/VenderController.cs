using Fiap.Leilao.Web.Models;
using Fiap.Leilao.Web.UnitsOfWork;
using Fiap.Leilao.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public ActionResult Vender(string mensage, string tipoMensagem)
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
                _unit.NegociacaoRepository.Cadastrar(negociacao);

                try
                {
                    _unit.Salvar();
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Erro {0}", e.Message);
                    return View(vViewModel);
                }
                return RedirectToAction("Vender", new { mensagem = "Produto adicionado a lista de vendas!", tipoMensagem = "alert alert-success" }); 
            }
            else
            {
                return View(vViewModel);
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