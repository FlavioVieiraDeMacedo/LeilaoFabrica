using Fiap.Leilao.Persistencia.UnitsOfWork;
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
        [HttpGet]
        public ActionResult Dashboard()
        {

            CompraViewModel compras = PopulaLista();
            return View(compras);
        }
        [HttpPost]
        public ActionResult Dashboard(CompraViewModel compraViewModel, int idNegociacao)
        {
            var comprar = _unit.NegociacaoRepository.BuscarPorId(idNegociacao);
            
            comprar.Status = "Aguardando Resposta";
            comprar.Id_Comprador = 1;
            comprar.Valor_Produto = compraViewModel.Valor_Produto;
            _unit.NegociacaoRepository.Alterar(comprar);
            _unit.Salvar();
            CompraViewModel compras = PopulaLista();
            return View(compras);
        }

        private CompraViewModel PopulaLista()
        {
            return new CompraViewModel()
            {
                Negociacoes = _unit.NegociacaoRepository.BuscarPor(n => n.Id_Comprador == null)

            };
        }
        public ActionResult Buscar(string nomeBusca)
        {
            var viewModel = new CompraViewModel()
            {
                Negociacoes = _unit.NegociacaoRepository.BuscarPor(n=> n.Produto.Nome == nomeBusca)
            };
            return PartialView("_tabela", viewModel);
        }
    }
}