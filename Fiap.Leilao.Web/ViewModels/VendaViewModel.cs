using Fiap.Leilao.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fiap.Leilao.Web.ViewModels
{
    public class VendaViewModel
    {

        #region FEILDs
        public int VendedorId { get; set; }
        public int ProdutoId { get; set; }
        public Decimal ValorVendedor { get; set; }
        public Decimal ValorProduto { get; set; }
        public string Status { get; set; }
        #endregion

        #region LISTs
        public ICollection<Produto> Produtos { get; set; }
        #endregion
    }
}