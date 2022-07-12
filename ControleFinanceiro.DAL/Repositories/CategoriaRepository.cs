using ControleFinanceiro.BLL.Models;
using ControleFinanceiro.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.DAL.Repositories
{
    public class CategoriaRepository : GenericRepository<Categoria>, ICategoriaRepository
    {
        private readonly Context _context;

        public CategoriaRepository(Context context):base(context)
        {
            _context = context;
        }

        public IQueryable<Categoria> CategoriasFilter(string nomeCategoria)
        {
            try
            {
                var entities = _context.Categorias.Include(c => c.Tipo).Where(c => c.Nome.Contains(nomeCategoria));
                return entities;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public new IQueryable<Categoria> FindAll()
        {
            try
            {
                return _context.Categorias.Include(c => c.Tipo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public new async Task<Categoria> FindById(int id)
        {
            try
            {
                var entity = await _context.Categorias.Include(c=>c.Tipo).FirstOrDefaultAsync(c => c.CategoriaId == id);
                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
