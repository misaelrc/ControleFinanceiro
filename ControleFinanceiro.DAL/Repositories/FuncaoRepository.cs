using ControleFinanceiro.BLL.Models;
using ControleFinanceiro.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.DAL.Repositories
{
    public class FuncaoRepository : GenericRepository<Funcao>, IFuncaoRepository
    {
        private readonly Context _context;
        private readonly RoleManager<Funcao> _funcoesManager;

        public FuncaoRepository(Context context, RoleManager<Funcao> funcoesManager) :base(context)
        {
            _context = context;
            _funcoesManager = funcoesManager;
        }

        public async Task AddFuncao(Funcao funcao)
        {
            try
            {
                await _funcoesManager.CreateAsync(funcao);
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<Funcao> FiltrarFuncoes(string nomeFuncao)
        {
            try
            {
                var entity = _context.Funcoes.Where(f => f.Name.Contains(nomeFuncao));
                return entity;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateFuncao(Funcao funcao)
        {
            try
            {
                Funcao f = await FindById(funcao.Id);
                f.Name = funcao.Name;
                f.NormalizedName = funcao.NormalizedName;
                f.Descricao = funcao.Descricao;

                await _funcoesManager.UpdateAsync(f);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
