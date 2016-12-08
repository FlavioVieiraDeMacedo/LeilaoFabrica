
using Fiap.Leilao.Dominio.Models;
using Fiap.Leilao.Persistencia.UnitsOfWork;
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
        public ActionResult Vender(string mensagem, string tipoMensagem)
        {
            var viewModel = new VendaViewModel()
            {
                Mensagem = mensagem,
                TipoMensagem = tipoMensagem,
                Produtos = ListarProdutos()
            };
            return View(viewModel);
        }

        #endregion

        #region POSTs

        [HttpPost]
        public ActionResult Vender(VendaViewModel vViewModel)
        {
            var produto = _unit.ProdutoRepository.BuscarPorId(vViewModel.ProdutoId);
            //tem problemas nessa parte
            //aparentemente o banco mesmo como nullable n aceita sem
            var usuario = _unit.UsuarioRepository.BuscarPorId(vViewModel.VendedorId);
            var usuario1 = _unit.UsuarioRepository.BuscarPorId(vViewModel.VendedorId);
            //popular tabela negociacao
            if (ModelState.IsValid)
            {
                var negociacao = new Negociacao()
                {
                    Id_Vendedor = vViewModel.VendedorId,                    
                    Id_Produto = vViewModel.ProdutoId,
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
                    Debug.WriteLine("Erro no Post do Vender" + e.Message);
                    vViewModel.Produtos = ListarProdutos();
                    return View(vViewModel);
                }
                vViewModel.Produtos = ListarProdutos();
                return RedirectToAction("Vender", new { mensagem = "Produto adicionado a lista de vendas!", tipoMensagem = "alert alert-success" }); 
            }
            else
            {
                vViewModel.Produtos = ListarProdutos();
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