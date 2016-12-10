
using Fiap.Leilao.Dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fiap.Leilao.Web.ViewModels
{
    public class ProdutoViewModel
    {
        public int Id { get; set; }
        public Nullable<decimal> Valor_Vendedor { get; set; }
        public Nullable<int> Id_Vendedor { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }
        public ICollection<Produto> Produtos { get; set; }
        public string Mensagem { get; set; }
        public string Nome2 { get; set; }
        public string Descricao2 { get; set; }
        public string Imagem2 { get; set; }
        public Nullable<decimal> Valor_Vendedor2 { get; set; }
        public Nullable<int> Id_Vendedor2 { get; set; }
    }
}
