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
        public ActionResult Painel(DashGenericoViewModel dGviewModel)
        {
            var viewModel = new DashGenericoViewModel()
            {
                ProdutosEmVenda = ListarProdutosEmVenda(),
                ProdutosEmCompra = ListarProdutosEmCompra()
            };
            return View(viewModel);
        }

        /// <summary>
        /// Função para verificar existência de cadastro com o e-mail informado
        /// utilizando ajax
        /// </summary>
        /// <param name="email">E-mail informado no input do cadastro de usuário</param>
        /// <returns>Se existe registro de um usuário com o e-mail</returns>
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
        public ActionResult RecusaProposta(int negociacaoId)
        {
            var negociacao = _unit.NegociacaoRepository.BuscarPorId(negociacaoId);
            negociacao.Id_Comprador = null;
            negociacao.Valor_Produto = null;
            negociacao.Status = "Em Aberto";
            _unit.NegociacaoRepository.Alterar(negociacao);
            _unit.Salvar();
            var viewModel = new DashGenericoViewModel()
            {
                ProdutosEmVenda = ListarProdutosEmVenda(),
                ProdutosEmCompra = ListarProdutosEmCompra()
            };
            return View("Painel", viewModel);
        }
        [HttpPost]
        public ActionResult AceitaProposta(int negociacao2Id)
        {
            var negociacao = _unit.NegociacaoRepository.BuscarPorId(negociacao2Id);
            negociacao.Status = "Vendido";
            _unit.NegociacaoRepository.Alterar(negociacao);
            _unit.Salvar();
            var viewModel = new DashGenericoViewModel()
            {
                ProdutosEmVenda = ListarProdutosEmVenda(),
                ProdutosEmCompra = ListarProdutosEmCompra()
            };
            return View("Painel", viewModel);
        }
        #endregion

        #region PRIVATEs
        private ICollection<Negociacao> ListarProdutosEmVenda()
        {
            return _unit.NegociacaoRepository.BuscarPor(n => n.Id_Vendedor == 1);
        }
        private ICollection<Negociacao> ListarProdutosEmCompra()
        {
            return _unit.NegociacaoRepository.BuscarPor(n => n.Id_Comprador == 1);
        }
        #endregion
    }
}

//author Júlio