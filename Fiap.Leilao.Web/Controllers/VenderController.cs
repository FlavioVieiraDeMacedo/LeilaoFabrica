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
    [Authorize]
    public class VenderController : Controller
    {
        private UnitOfWork _unit = new UnitOfWork();
        [HttpGet]
        public ActionResult DashBoard(string msg)
        {
            var usuario = _unit.UsuarioRepository.BuscarPor(a => a.Email == User.Identity.Name);
            var id = usuario.First().Id;
            var viewModel = new ProdutoViewModel()
            {
                Mensagem = msg,
                Produtos = _unit.ProdutoRepository.BuscarPor(a=>a.Id_Vendedor == id)
        };
            return View(viewModel);
        }
        [HttpGet]
        public ActionResult Buscar(string nomeBusca)
        {
            var usuario = _unit.UsuarioRepository.BuscarPor(a => a.Email == User.Identity.Name);
            var id = usuario.First().Id;
            var lista = _unit.ProdutoRepository.BuscarPor(a => a.Nome.Contains(nomeBusca)&& a.Id_Vendedor == id);
            var viewModel = new ProdutoViewModel()
            {
                Produtos = lista
            };
            return PartialView("_tabela", viewModel);

        }
        [HttpPost]
        public ActionResult Cadastrar(ProdutoViewModel viewModel)
        {
            var usuario = _unit.UsuarioRepository.BuscarPor(a => a.Email == User.Identity.Name);
            Produto produto = new Produto()
            {
               Id=viewModel.Id,
               Nome=viewModel.Nome,
               Descricao=viewModel.Descricao,
               Imagem=viewModel.Imagem,
               Id_Vendedor= usuario.First().Id,
               Valor_Vendedor=viewModel.Valor_Vendedor,
               Status_Produto="Vendendo"
            };
            _unit.ProdutoRepository.Cadastrar(produto);
            _unit.Salvar();
            return RedirectToAction("DashBoard");
        }
        [HttpPost]
        public ActionResult Excluir(int produtoId)
        {
            var negociacoes = _unit.NegociacaoRepository.BuscarPor(a => a.Id_Produto == produtoId);
            foreach (var item in negociacoes)
            {
                _unit.NegociacaoRepository.Remover(item.Id);
            }
            
            _unit.ProdutoRepository.Remover(produtoId);
            _unit.Salvar();
            return RedirectToAction("DashBoard", new { msg = "Produto Deletado com sucesso!" });
        }
        [HttpPost]
        public ActionResult Editar(ProdutoViewModel viewModel)
        {
            Produto Produto = new Produto()
            {
                Id=viewModel.Id,
                Nome = viewModel.Nome2,
                Descricao = viewModel.Descricao2,
                Imagem = viewModel.Imagem2,
                Valor_Vendedor=viewModel.Valor_Vendedor2,
                Id_Vendedor=viewModel.Id_Vendedor2,
                Status_Produto=viewModel.Status_Produto
            };
            _unit.ProdutoRepository.Alterar(Produto);
            _unit.Salvar();
            return RedirectToAction("DashBoard", new { msg = "Produto Alterado com sucesso!" });
        }
    }
}