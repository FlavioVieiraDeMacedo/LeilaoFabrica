using Fiap.Leilao.Dominio.Models;
using Fiap.Leilao.Persistencia.UnitsOfWork;
using Fiap.Leilao.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;

namespace Fiap.Leilao.Web.Controllers
{
    
    public class UsuarioController : Controller
    {
        #region FIELDs
        private UnitOfWork _unit = new UnitOfWork();
        #endregion

        #region GETS
        [HttpGet]
        public ActionResult Cadastrar(string mensagem, string tipoMensagem)
        {
            var viewModel = new UsuarioViewModel()
            {
                Mensagem = mensagem,
                TipoMensagem = tipoMensagem
            };
            return View(viewModel);
        }

        //dashboard generico
        [HttpGet]
        public ActionResult Dashboard(DashGenericoViewModel dGviewModel)
        {
            var viewModel = new DashGenericoViewModel()
            {

                ProdutosEmVenda = ListarProdutosEmVenda(),
                ProdutosEmCompra = ListarProdutosEmCompra(),
                ResponderProposta = ContaVendasPendentes(),
                Vendido = ContaVendidos(),
                Vendendo = ContaVendendo(),
                AguardandoResposta = ContaComprasPendentes(),
                Finalizado = ContaComprados(),
                Negado = ContaComprasNegados()

            };
            return View(viewModel);
        }
        
        [HttpGet]
        public ActionResult VerificarEmail(string email)
        {
            var usuario = _unit.UsuarioRepository.BuscarPor(u=>u.Email == email);
            return Json(new { ok = usuario.Any() }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region POSTs
        [HttpPost]
        public ActionResult Cadastrar(UsuarioViewModel uViewModel)
        {
            if (ModelState.IsValid)
            {
                var usuario = new Usuario()
                {
                    Nome = uViewModel.Nome,
                    Email = uViewModel.Email,
                    Senha = uViewModel.Senha,
                    CPF = uViewModel.Cpf,
                    CEP = uViewModel.Cep,
                    Numero = uViewModel.Numero,
                    Complemento = uViewModel.Complemento,
                    Dt_Nascimento = uViewModel.DataNascimento,
                    Telefone = uViewModel.Telefone
                };

                _unit.UsuarioRepository.Cadastrar(usuario);

                try
                {
                    _unit.Salvar();
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Erro {0}", e.Message);
                    return View(uViewModel);                    
                }

                return RedirectToAction("Cadastrar", new { mensagem = "Cadastro Realizado!", tipoMensagem = "alert alert-success" });
            }
            else
            {
                return View(uViewModel);
            }
            
        }
        [HttpPost]
        public ActionResult RecusaProposta(int produtoId)
        {
            var produto = _unit.ProdutoRepository.BuscarPorId(produtoId);
            var negociacao = _unit.NegociacaoRepository.BuscarPorId(produto.Negociacaos.Last().Id);
            produto.Status_Produto = "Vendendo";
            negociacao.Status = "Negado";
            _unit.ProdutoRepository.Alterar(produto);
            _unit.NegociacaoRepository.Alterar(negociacao);
            _unit.Salvar();
            var viewModel = new DashGenericoViewModel()
            {
                ProdutosEmVenda = ListarProdutosEmVenda(),
                ProdutosEmCompra = ListarProdutosEmCompra()
            };
            return View("Dashboard", viewModel);
        }
        [HttpPost]
        public ActionResult AceitaProposta(int produtoId2)
        {
            var produto = _unit.ProdutoRepository.BuscarPorId(produtoId2);
            var negociacao = _unit.NegociacaoRepository.BuscarPorId(produto.Negociacaos.Last().Id);
            produto.Status_Produto = "Vendido";
            negociacao.Status = "Vendido";
            _unit.ProdutoRepository.Alterar(produto);
            _unit.NegociacaoRepository.Alterar(negociacao);
            _unit.Salvar();
            var viewModel = new DashGenericoViewModel()
            {
                ProdutosEmVenda = ListarProdutosEmVenda(),
                ProdutosEmCompra = ListarProdutosEmCompra()
            };
            return View("Dashboard", viewModel);
        }
        #endregion

        #region PRIVATEs
        private ICollection<Produto> ListarProdutosEmVenda()
        {
            return _unit.ProdutoRepository.BuscarPor(n => n.Id_Vendedor == 3); 
        }
        private ICollection<Negociacao> ListarProdutosEmCompra()
        {
            return _unit.NegociacaoRepository.BuscarPor(n => n.Id_Comprador == 3);

        }
        private int ContaVendendo()
        {
            var a = _unit.ProdutoRepository.BuscarPor(n => n.Id_Vendedor == 3 && n.Status_Produto == "Vendendo");
            return a.Count();
        }
        private int ContaVendidos()
        {
            var a = _unit.ProdutoRepository.BuscarPor(n => n.Id_Vendedor == 3 && n.Status_Produto == "Vendido");
            return a.Count();
        }
        private int ContaVendasPendentes()
        {
            var a = _unit.ProdutoRepository.BuscarPor(n => n.Id_Vendedor == 3 && n.Status_Produto == "Em negociação");
            return a.Count();
        }
        private int ContaComprasNegados()
        {
            var a = _unit.NegociacaoRepository.BuscarPor(n => n.Id_Comprador == 3 && n.Status == "Negado");
            return a.Count();
        }
        private int ContaComprasPendentes()
        {
            var a = _unit.NegociacaoRepository.BuscarPor(n => n.Id_Comprador == 3 && n.Status == "Aguardando Resposta");
            return a.Count();
        }
        private int ContaComprados()
        {
            var a = _unit.NegociacaoRepository.BuscarPor(n => n.Id_Comprador == 3 && n.Status == "Vendido");
            return a.Count();
        }
        #endregion
    }
}

