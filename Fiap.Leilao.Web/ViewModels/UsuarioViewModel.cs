using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fiap.Leilao.Web.ViewModels
{
    public class UsuarioViewModel
    {
        public string Mensagem { get; set; }
        public string TipoMensagem { get; set; }

        #region FIELDS

        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Nome { get; set; }
        [Required]
        [StringLength(255)]
        public string Email { get; set; }
        [Required]
        [StringLength(8)]
        public string Senha { get; set; }
        [Required]
        [StringLength(14)]
        public string Cpf { get; set; }
        [Required]
        [StringLength(11)]
        public string Cep { get; set; }
        [Required]
        [StringLength(5)]
        public int Numero { get; set; }
        [StringLength(255)]
        public string Complemento { get; set; }
        [Required]
        public DateTime DataNascimento { get; set; }
        [Required]
        [StringLength(13)]
        public string Telefone { get; set; }

        #endregion

    }
}