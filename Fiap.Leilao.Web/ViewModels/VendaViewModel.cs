using Fiap.Leilao.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fiap.Leilao.Web.ViewModels
{
    public class VendaViewModel
    {

        public string Mensagem { get; set; }
        public string TipoMensagem { get; set; }

        #region FEILDs
        public int VendedorId { get; set; }
        public int ProdutoId { get; set; }
        [Display(Name = "Valor para Venda")]
        public Decimal ValorVendedor { get; set; }
        [Display(Name = "Valor sugerido do Produto")]
        public Decimal ValorProduto { get; set; }
        public string Status { get; set; }
        #endregion

        #region LISTs
        public ICollection<Produto> Produtos { get; set; }
        #endregion
    }
}