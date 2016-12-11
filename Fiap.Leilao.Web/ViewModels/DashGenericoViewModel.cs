using Fiap.Leilao.Dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fiap.Leilao.Web.ViewModels
{
    public class DashGenericoViewModel
    {
        #region LISTs
        public ICollection<Produto> ProdutosEmVenda { get; set; }
        public ICollection<Negociacao> ProdutosEmCompra { get; set; }
        public int Vendido { get; set; }
        public int Vendendo { get; set; }
        public int ResponderProposta { get; set; }
        public int Negado { get; set; }
        public int Finalizado { get; set; }
        public int AguardandoResposta { get; set; }
        #endregion
    }
}