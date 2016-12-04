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
    public class ProdutoController : Controller
    {
        private UnitOfWork _unit = new UnitOfWork();
        [HttpGet]
        public ActionResult Index(string msg)
        {
            var viewModel = new ProdutoViewModel()
            {
                Mensagem = msg,
                Produtos = _unit.ProdutoRepository.Listar()
            };
            return View(viewModel);
        }
        [HttpGet]
        public ActionResult Buscar(string nomeBusca)
        {
            var lista = _unit.ProdutoRepository.BuscarPor(a => a.Nome.Contains(nomeBusca));
            var viewModel = new ProdutoViewModel()
            {
                Produtos = lista
            };
            return PartialView("_tabela", viewModel);

        }
        [HttpPost]
        public ActionResult Cadastrar(ProdutoViewModel viewModel)
        {
            Produto produto = new Produto()
            {
               Id=viewModel.Id,
               Nome=viewModel.Nome,
               Descricao=viewModel.Descricao,
               Imagem=viewModel.Imagem
            };
            _unit.ProdutoRepository.Cadastrar(produto);
            _unit.Salvar();
            return RedirectToAction("Index", new { msg = "Produto Cadastrado com sucesso!" });
        }
        [HttpPost]
        public ActionResult Excluir(int produtoId)
        {
            _unit.ProdutoRepository.Remover(produtoId);
            _unit.Salvar();
            return RedirectToAction("Index", new { msg = "Produto Deletado com sucesso!" });
        }
        [HttpPost]
        public ActionResult Editar(ProdutoViewModel viewModel)
        {
            Produto Produto = new Produto()
            {
                Id=viewModel.Id,
                Nome = viewModel.Nome2,
                Descricao = viewModel.Descricao2,
                Imagem = viewModel.Imagem2
            };
            _unit.ProdutoRepository.Alterar(Produto);
            _unit.Salvar();
            return RedirectToAction("Index", new { msg = "Produto Alterado com sucesso!" });
        }
    }
}