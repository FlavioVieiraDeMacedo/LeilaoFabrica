using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fiap.Leilao.Web.ViewModels
{
    public class UserViewModel
    {
        public string Mensagem { get; set; }
        #region LogIn
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [HiddenInput]
        public string ReturnUrl { get; set; }
        #endregion
        #region Informacoes Pessoais
        public string Nome { get; set; }
        [Required]
        //[StringLength(14)]
        public string Cpf { get; set; }
        [Required]
        // [StringLength(11)]
        public string Cep { get; set; }
        [Required]
        // [StringLength(5)]
        public int Numero { get; set; }
        // [StringLength(255)]
        public string Complemento { get; set; }
        [Required]
        public DateTime DataNascimento { get; set; }
        [Required]
        // [StringLength(13)]
        public string Telefone { get; set; }
        #endregion
    }
}