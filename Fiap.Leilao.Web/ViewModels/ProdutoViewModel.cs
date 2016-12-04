﻿using Fiap.Leilao.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fiap.Leilao.Web.ViewModels
{
    public class ProdutoViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }
        public ICollection<Produto> Produtos { get; set; }
        public string Mensagem { get; set; }
        public string Nome2 { get; set; }
        public string Descricao2 { get; set; }
        public string Imagem2 { get; set; }
    }
}
