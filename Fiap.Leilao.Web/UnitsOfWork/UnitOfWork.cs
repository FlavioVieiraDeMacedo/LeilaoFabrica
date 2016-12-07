using Fiap.Leilao.Web.Models;
using Fiap.Leilao.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fiap.Leilao.Web.UnitsOfWork
{
    public class UnitOfWork
    {
        #region FIELDS
        private LeilaoContext _context = new LeilaoContext();

        private IGenericRepository<Usuario> _usuarioRepository;
        private IGenericRepository<Produto> _produtoRepository;
        private IGenericRepository<Negociacao> _negociacaoRepository;
        #endregion

        #region GETS

        public IGenericRepository<Produto> ProdutoRepository
        {
            get
            {
                if (_produtoRepository == null)
                {

                    _produtoRepository = new GenericRepository<Produto>(_context);
                }
                return _produtoRepository;
            }
        }
        public IGenericRepository<Usuario> UsuarioRepository
        {
            get
            {
                if (_usuarioRepository == null)
                {

                    _usuarioRepository = new GenericRepository<Usuario>(_context);
                }
                return _usuarioRepository;
            }
        }
        public IGenericRepository<Negociacao> NegociacaoRepository
        {
            get
            {
                if (_negociacaoRepository == null)
                {

                    _negociacaoRepository = new GenericRepository<Negociacao>(_context);
                }
                return _negociacaoRepository;
            }
        }

        #endregion

        #region DISPOSE

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
            GC.SuppressFinalize(this);
        }

        #endregion

        #region SAVE

        public void Salvar()
        {
            _context.SaveChanges();
        }

        #endregion

    }
}
