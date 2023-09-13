using Microsoft.AspNetCore.Mvc;
using RpgApi.Models;
using RpgApi.Models.Enuns;
using System.Collections.Generic;

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class PersonagensExercicioController : ControllerBase
    {
        private static List<Personagem> personagens = new List<Personagem>()
        {
            //Colar os objetos da lista do chat aqui
            new Personagem() { Id = 1, Nome = "Frodo", PontosVida=100, Forca=17, Defesa=23, Inteligencia=33, Classe=ClasseEnum.Cavaleiro},
            new Personagem() { Id = 2, Nome = "Sam", PontosVida=100, Forca=15, Defesa=25, Inteligencia=30, Classe=ClasseEnum.Cavaleiro},
            new Personagem() { Id = 3, Nome = "Galadriel", PontosVida=100, Forca=18, Defesa=21, Inteligencia=35, Classe=ClasseEnum.Clerigo },
            new Personagem() { Id = 4, Nome = "Gandalf", PontosVida=100, Forca=18, Defesa=18, Inteligencia=37, Classe=ClasseEnum.Mago },
            new Personagem() { Id = 5, Nome = "Hobbit", PontosVida=100, Forca=20, Defesa=17, Inteligencia=31, Classe=ClasseEnum.Cavaleiro },
            new Personagem() { Id = 6, Nome = "Celeborn", PontosVida=100, Forca=21, Defesa=13, Inteligencia=34, Classe=ClasseEnum.Clerigo },
            new Personagem() { Id = 7, Nome = "Radagast", PontosVida=100, Forca=25, Defesa=11, Inteligencia=35, Classe=ClasseEnum.Mago }
        };





        [HttpGet("GetByNome/{nome}")]
        public IActionResult GetByNome(string nome)
        {
            List<Personagem> listaBusca = personagens.FindAll(p => p.Nome.Contains(nome));

            if (listaBusca.Count != 0)
            {
                return Ok(listaBusca);
            }
            else
                return BadRequest("Request NotFound");
        }

        [HttpPost("PostValidacao")]
        public IActionResult PostValidacao(Personagem p)
        {
            if (p.Defesa < 10 || p.Inteligencia > 30)
            {
                return BadRequest("A defensa deve ser >= 10 e a inteligência <= 30");
            }
            else
                return Ok();
        }

        [HttpPost("PostValidacaoMago")]
        public IActionResult PostValidacaoMago(Personagem novoPersonagem)
        {

            {
                if (novoPersonagem.Inteligencia <= 35)
                    return BadRequest("Inteligencia não pode ter o valor menor ou igual a 35(TRINTA E CINCO).");

                personagens.Add(novoPersonagem);
                return Ok(personagens);

            }
        }

        [HttpGet("GetClerigoMago")]
        public IActionResult GetClerigoMago()
        {
            List<Personagem> cavaleiros = personagens.Where(p => p.Classe == ClasseEnum.Cavaleiro).ToList();
            personagens.RemoveAll(p => p.Classe == ClasseEnum.Cavaleiro);
            List<Personagem> listaFinal = personagens.OrderByDescending(p => p.PontosVida).ToList();
            return Ok(listaFinal);
        }


        [HttpGet("GetEstatisticas")]
        public IActionResult GetEstatisticas()
        {
            int quantidadePersonagens = personagens.Count;
            int somatorioInteligencia = personagens.Sum(p => p.Inteligencia);
            return Ok(new { QuantidadePersonagens = quantidadePersonagens, SomatorioInteligencia = somatorioInteligencia });
        }

        [HttpGet("GetByClasse/{classe}")]
        public IActionResult GetByClasse(ClasseEnum classe)
        {
            List<Personagem> personagensDaClasse = personagens.FindAll(p => p.Classe == classe);
            return Ok(personagensDaClasse);
        }


    }

}
