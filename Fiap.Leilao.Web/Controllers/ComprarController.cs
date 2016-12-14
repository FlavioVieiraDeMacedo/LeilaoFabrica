using Fiap.Leilao.Dominio.Models;
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
        public ActionResult Dashboard(CompraViewModel compraViewModel)
        {

            var negociacao = new Negociacao
            {
                Id_Produto = compraViewModel.Id_Produto,
                Valor_Produto=compraViewModel.Valor_Produto,
                Id_Comprador = 3, // alterar na autenticacao
                Status = "Aguardando Resposta"
            };
            var produto = _unit.ProdutoRepository.BuscarPorId((int)compraViewModel.Id_Produto);
            produto.Status_Produto = "Em negociação";
            _unit.ProdutoRepository.Alterar(produto);
            _unit.NegociacaoRepository.Cadastrar(negociacao);
            _unit.Salvar();
            CompraViewModel compras = PopulaLista();
            return RedirectToAction("Dashboard", "Usuario");

        }

        private CompraViewModel PopulaLista()
        {
            return new CompraViewModel()
            {
                
                Produtos = _unit.ProdutoRepository.BuscarPor(p=>p.Status_Produto=="Vendendo")

            };
        }
        
        [HttpGet]
        public ActionResult Buscar(string nomeBusca)
        {
            var lista = _unit.ProdutoRepository.BuscarPor(a => a.Nome.Contains(nomeBusca) && a.Status_Produto == "Vendendo");
            var viewModel = new CompraViewModel()
            {
                Produtos = lista
            };
            return PartialView("_tabela", viewModel);

        }
    }
}