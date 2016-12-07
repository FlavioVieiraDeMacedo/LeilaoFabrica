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
        public ICollection<Produto> Produtos { get; set; }
        #endregion
    }
}