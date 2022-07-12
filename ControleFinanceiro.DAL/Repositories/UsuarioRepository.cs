using ControleFinanceiro.BLL.Models;
using ControleFinanceiro.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.DAL.Repositories
{
    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {
        private readonly Context _context;
        private readonly UserManager<Usuario> _usuariosManager;
        private readonly SignInManager<Usuario> _signInManager;
        public UsuarioRepository(Context context, UserManager<Usuario> usuariosManager, SignInManager<Usuario> signInManager) : base(context)
        {
            _context = context;
            _usuariosManager = usuariosManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> CriarUsuario(Usuario usuario, string senha)
        {
            try
            {
                return await _usuariosManager.CreateAsync(usuario, senha);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task IncluirUsuarioEmFuncao(Usuario usuario, string funcao)
        {
            try
            {
                await _usuariosManager.AddToRoleAsync(usuario, funcao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task LogarUsuario(Usuario usuario, bool lembrar)
        {
            try
            {
                await _signInManager.SignInAsync(usuario, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IList<string>> PegarFuncoesUsuario(Usuario usuario)
        {
            try
            {
                return await _usuariosManager.GetRolesAsync(usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> PegarQuantidadeUsuariosRegistrados()
        {
            try
            {
                return await _context.Usuarios.CountAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Usuario> PegarUsuarioPeloEmail(string email)
        {
            try
            {
                return await _usuariosManager.FindByEmailAsync(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
