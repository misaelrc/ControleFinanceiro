using ControleFinanceiro.API.ViewModels;
using ControleFinanceiro.BLL.Models;
using ControleFinanceiro.DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncoesController : ControllerBase
    {
        private readonly IFuncaoRepository _funcaoRepository;

        public FuncoesController(IFuncaoRepository funcaoRepository)
        {
            _funcaoRepository = funcaoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Funcao>>> GetFuncoes()
        {
            return await _funcaoRepository.FindAll().ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Funcao>> GetFuncao(string id)
        {
            var funcao = await _funcaoRepository.FindById(id);

            if (funcao == null)
            {
                return NotFound();
            }
            return funcao;
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> PutFuncao(string id, FuncoesViewModel funcao)
        {
            if (id != funcao.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                Funcao f = new Funcao
                {
                    Id = funcao.Id,
                    Name = funcao.Name,
                    Descricao = funcao.Descricao
                };

                await _funcaoRepository.UpdateFuncao(f);

                return Ok(new { message = $"Função {f.Name} atualizada com sucesso" });
            }

            return BadRequest(ModelState);
        }

        [HttpPost]

        public async Task<ActionResult<Funcao>> PostFuncao(FuncoesViewModel funcao)
        {
            if (ModelState.IsValid)
            {
                Funcao f = new Funcao
                {
                    Name = funcao.Name,
                    Descricao = funcao.Descricao
                };

                await _funcaoRepository.AddFuncao(f);

                return Ok(new { message = $"Função {f.Name} adicionada com sucesso" });
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<Funcao>> DeleteFuncao(string id)
        {
            var funcao = await _funcaoRepository.FindById(id);
            if (funcao == null)
            {
                return NotFound();
            }

            await _funcaoRepository.Delete(funcao);
            return Ok(new { message = $"Função {funcao.Name} excluída com sucesso" });
        }

        [HttpGet("FiltrarFuncoes/{nomeFuncao}")]
        public async Task<ActionResult<IEnumerable<Funcao>>> FiltrarFuncoes(string nomeFuncao){
            return await _funcaoRepository.FiltrarFuncoes(nomeFuncao).ToListAsync();
        }



    }
}
