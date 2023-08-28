using api.Business;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoboController : ControllerBase
    {
        private static Robo _robo = new Robo();

        /// <summary>
        /// Metodo que retorna os dados do robo atualizado
        /// </summary>
        /// <returns></returns>
        [HttpGet("PegaRobo")]
        public Robo PegaRobo()
        {
            return _robo;
        }

        /// <summary>
        /// Metodo que valida e atualiza as informações do robo
        /// </summary>
        /// <param name="roboAtualizado"></param>
        /// <returns></returns>
        [HttpPut("AtualizaRobo")]
        public MensagensErroRobo AtualizaRobo(Robo roboAtualizado)
        {
            MensagensErroRobo mensagensErro = ValidacaoRobo.ValidarMovimentacaoRobo(roboAtualizado, _robo);

            mensagensErro.PossuiErros = ValidacaoRobo.verificaErros(mensagensErro);

            // Se não existirem erros atualizamos as informações do robo
            if (!mensagensErro.PossuiErros)
                _robo = roboAtualizado;

            return mensagensErro;
        }
    }
}