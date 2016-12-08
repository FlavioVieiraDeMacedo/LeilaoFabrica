
using Fiap.Leilao.Dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fiap.Leilao.Web.ViewModels
{
    public class CompraViewModel
    {
        public int Id { get; set; }
        public Nullable<int> Id_Vendedor { get; set; }
        public Nullable<int> Id_Produto { get; set; }
        public Nullable<int> Id_Comprador { get; set; }
        public Nullable<decimal> Valor_Vendedor { get; set; }
        public Nullable<decimal> Valor_Produto { get; set; }
        public string Status { get; set; }

        public ICollection<Negociacao> Negociacoes { get; set; }
    }
}