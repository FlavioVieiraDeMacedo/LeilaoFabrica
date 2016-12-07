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
        #endregion
    }
}

//author Júlio