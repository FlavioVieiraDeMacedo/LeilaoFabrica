﻿using Fiap.Leilao.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fiap.Leilao.Web.ViewModels
{
    public class DashGenericoViewModel
    {
        #region LISTs
        public ICollection<Negociacao> ProdutosEmVenda { get; set; }
        #endregion
    }
}