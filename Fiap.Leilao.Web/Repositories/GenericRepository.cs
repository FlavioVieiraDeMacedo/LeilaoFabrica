using Fiap.Leilao.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Fiap.Leilao.Web.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        #region FIELDS
        protected LeilaoContext _context;
        protected DbSet<T> _dbSet;
        #endregion

        public GenericRepository(LeilaoContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Alterar(T entidade)
        {
            _context.Entry(entidade).State = EntityState.Modified;
        }

        public ICollection<T> BuscaPor(Expression<Func<T, bool>> filtro)
        {
            return _dbSet.Where(filtro).ToList();
        }

        public T BuscarPorId(int id)
        {
            return _dbSet.Find(id);
        }

        public void Cadastrar(T entidade)
        {
            _dbSet.Add(entidade);
        }

        public ICollection<T> Listar()
        {
            return _dbSet.ToList();
        }

        public void Remover(int id)
        {
            var entity = BuscarPorId(id);
            _dbSet.Remove(entity);
        }
    }
}