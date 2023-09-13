using Microsoft.AspNetCore.Mvc;
using RpgApi.Models;
using RpgApi.Models.Enuns;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RpgApi.Data;

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]

    public class PersonagensController : ControllerBase
    {
      //Programação de toda a classe será aqui 
      private readonly DataContext _context; //Declaração de atributo

      public PersonagensController(DataContext context)
      {
        //Inicialização doi atributo a partir de um parâmetro
        _context = context;
      }

    [HttpGet("{id}")] //Buscar pelo id
    public async Task<IActionResult> GetSingle(int id)
    {
        try{
            Personagem p = await _context.TB_PERSONAGENS
            .FirstOrDefaultAsync(pBusca => pBusca.Id == id);

            return Ok(p);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet ("GetAll")]
    public async Task<IActionResult> Get()
{
    try
    {
        List<Personagem> lista = await _context.TB_PERSONAGENS.ToListAsync();
        return Ok(lista);
    }
    catch (System.Exception ex)
    {
        return BadRequest(ex.Message);
    }
}

    [HttpPost]
    public async Task<IActionResult> Add(Personagem novoPersonagem)
    {
        try
        {
            if (novoPersonagem.PontosVida > 100)
            {
                throw new Exception("Pontos de vida não pode ser maior que 100");
            }
            await _context.TB_PERSONAGENS.AddAsync(novoPersonagem);
            await _context.SaveChangesAsync();
            
            return Ok(novoPersonagem.Id);
        }
        catch (System.Exception ex)
        {
            return BadRequest (ex.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update(Personagem novoPersonagem)
    {
        try
        {
            if (novoPersonagem.PontosVida > 100)
            {
                throw new System.Exception("Pontos de vida não pode ser maior que 100");
            }
            _context.TB_PERSONAGENS.Update(novoPersonagem);
            int linhasAfetadas = await _context.SaveChangesAsync();

            return Ok(linhasAfetadas);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
    {
        try
        {
            Personagem pRemover = await _context.TB_PERSONAGENS.FirstOrDefaultAsync(p => p.Id == id);

            _context.TB_PERSONAGENS.Remove(pRemover);
            int linhasAfetadas = await _context.SaveChangesAsync();
            return Ok(linhasAfetadas);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    }//Fim da classe do tipo controller
}