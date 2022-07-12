using ControleFinanceiro.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.DAL.Interfaces
{
    public interface ICategoriaRepository : IGenericRepository<Categoria>
    {
        new IQueryable<Categoria> FindAll();

        new Task<Categoria> FindById(int id);

        IQueryable<Categoria> CategoriasFilter(string nomeCategoria);
    }
}
