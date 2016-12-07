using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fiap.Leilao.Web.ViewModels
{
    public class UsuarioViewModel
    {

        #region FIELDS

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Cpf { get; set; }
        public string Cep { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }

        #endregion

    }
}