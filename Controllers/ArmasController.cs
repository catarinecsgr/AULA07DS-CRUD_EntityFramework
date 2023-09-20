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

    public class ArmasController : ControllerBase
    {
      //Programação de toda a classe será aqui 
      private readonly DataContext _context; //Declaração de atributo

      public ArmasController(DataContext context)
      {
        //Inicialização do atributo a partir de um parâmetro
        _context = context;
      }


        [HttpGet ("GetAll")]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Armas> lista = await _context.TB_ARMAS.ToListAsync();
                return Ok(lista);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> Add(Armas novaArma)
        {
            try
            {
               
                await _context.TB_ARMAS.AddAsync(novaArma);
                await _context.SaveChangesAsync();
                
                return Ok(novaArma.Id);
            }
            catch (System.Exception ex)
            {
                return BadRequest (ex.Message);
            }
        }
        
         [HttpPut]
    public async Task<IActionResult> Update(Armas novaArma)
    {
        try
        {
        
            _context.TB_ARMAS.Update(novaArma);
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
            Armas aRemover = await _context.TB_ARMAS.FirstOrDefaultAsync(a => a.Id == id);

            _context.TB_ARMAS.Remove(aRemover);
            int linhasAfetadas = await _context.SaveChangesAsync();
            return Ok(linhasAfetadas);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    }
}