using Fiap.Leilao.Dominio.Models;
using Fiap.Leilao.Persistencia.UnitsOfWork;
using Fiap.Leilao.Web.App_Start;
using Fiap.Leilao.Web.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Fiap.Leilao.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> userManager;
        public UserController()
        {
            this.userManager = IdentityConfig.UserManagerFactory.Invoke();
        }
        private UnitOfWork _unit = new UnitOfWork();
        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing && userManager != null)
            {
                userManager.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
        #region Private
        private IAuthenticationManager GetAuthenticationManager()
        {
            var ctx = Request.GetOwinContext();
            return ctx.Authentication;
        }
        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("DashBoard", "Usuario");
            }
            return returnUrl;
        }
        #endregion
        #region Registrar
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var user = new User
            {
                UserName = model.Email
            };
            var usuario = new Usuario()
            {
                Nome = model.Nome,
                Email = model.Email,
                Senha = model.Password,
                CPF = model.Cpf,
                CEP = model.Cep,
                Numero = model.Numero,
                Complemento = model.Complemento,
                Dt_Nascimento = model.DataNascimento,
                Telefone = model.Telefone
            };
            var result = await userManager.CreateAsync(user, model.Password);
            
            if (result.Succeeded)
            {
                _unit.UsuarioRepository.Cadastrar(usuario);
                _unit.Salvar();

                var identity = await userManager.CreateIdentityAsync(
                user, DefaultAuthenticationTypes.ApplicationCookie);
                GetAuthenticationManager().SignIn(identity);
                return RedirectToAction("LogIn", "User");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("deu ruim ", error);
            }
            return View();
        }
        #endregion
        #region Login
        [HttpGet]
        public ActionResult LogIn(string returnUrl)
        {
            var model = new UserViewModel
            {
                ReturnUrl = returnUrl
            };
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> LogIn(UserViewModel model)
        {
            var user = await userManager.FindAsync(model.Email, model.Password);
            if (user != null)
            {
                var identity = await userManager.CreateIdentityAsync(
                user, DefaultAuthenticationTypes.ApplicationCookie);
                GetAuthenticationManager().SignIn(identity);
                return Redirect(GetRedirectUrl(model.ReturnUrl));
            }
            ModelState.AddModelError("", "Usuário e/ou Senha inválidos");
            return View("Register");
        }

        #endregion
        #region LogOut
        public ActionResult LogOut()
        {
            GetAuthenticationManager().SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("LogIn", "User");
        }
        #endregion
    }
}