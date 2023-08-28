using api.Business;
using api.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Testes.Testes
{
    public class TestesRobo
    {
        /// <summary>
        /// Verifica se não será permitido a alteração do pulso
        /// sem que o cotovelo esteja fortemente contraído.
        /// </summary>
        [Fact]
        public static void ValidacaoRobo_ValidarMovimentacaoRobo_RetornarErroMovimentoPulso()
        {
            // Arrange
            Robo roboAntigo = new Robo();
            Robo roboNovo = new Robo();
            roboNovo.BracoEsquerdo.Pulso = Enums.EstadoPulso.RotacaoPositiva45;

            //Act
            MensagensErroRobo mensagens = ValidacaoRobo.ValidarMovimentacaoRobo(roboNovo, roboAntigo);

            //Assert
            mensagens.PulsoEsquerdo.Should().NotBeNullOrWhiteSpace();
        }

        /// <summary>
        /// Verifica se o usuario sera proibido de rotacionar a cabeça, caso a Inclinação da Cabeça não esteja em
        /// estado Para Baixo.
        /// </summary>
        [Fact]
        public static void ValidacaoRobo_ValidarMovimentacaoRobo_RetornarErroRotacionarCabeca()
        {
            // Arrange
            Robo roboAntigo = new Robo();
            Robo roboNovo = new Robo();
            roboNovo.Cabeca.Inclinacao = Enums.EstadoInclinacao.ParaBaixo;
            roboNovo.Cabeca.Rotacao = Enums.EstadoRotacao.Rotação45;

            //Act
            MensagensErroRobo mensagens = ValidacaoRobo.ValidarMovimentacaoRobo(roboNovo, roboAntigo);

            //Assert
            mensagens.Rotacao.Should().NotBeNullOrWhiteSpace();
        }

        /// <summary>
        /// Verifica se não será permitido pular uma posição
        /// </summary>
        [Fact]
        public static void ValidacaoRobo_ValidarTransicao_RetornarErroProgressao()
        {
            //Act
            var mensagem = ValidacaoRobo.ValidarTransicao<Enums.EstadoRotacao>(
                                                        (int)Enums.EstadoRotacao.EmRepouso,
                                                        (int)Enums.EstadoRotacao.RotacaoNegativa90);

            //Assert
            mensagem.Should().NotBeNullOrWhiteSpace();
        }

        /// <summary>
        /// Verifica se não é permitido enviar um estado invalido
        /// </summary>
        [Fact]
        public static void ValidacaoRobo_ValidarTransicao_RetornarErroEstadoInvalido()
        {
            //Act
            var mensagem = ValidacaoRobo.ValidarTransicao<Enums.EstadoRotacao>(9,
                                                        (int)Enums.EstadoRotacao.RotacaoNegativa90);

            //Assert
            mensagem.Should().NotBeNullOrWhiteSpace();
        }
    }
}
