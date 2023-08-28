using api.Enums;
using api.Models;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Reflection;

namespace api.Business
{
    public class ValidacaoRobo
    {
        /// <summary>
        /// Metodo com as regras para a atualização do robo
        /// </summary>
        /// <param name="roboAtualizado"></param>
        /// <param name="roboAntigo"></param>
        /// <returns></returns>
        public static MensagensErroRobo ValidarMovimentacaoRobo(Robo roboAtualizado, Robo roboAntigo)
        {
            MensagensErroRobo mensagensErro = new MensagensErroRobo();

            #region Braços
            // Caso realmente exitir alguma alteração realizamos a validação
            if (roboAtualizado.BracoEsquerdo != roboAntigo.BracoEsquerdo)
            {
                var mensagemPulso = ValidarTransicao<EstadoPulso>((int)roboAtualizado.BracoEsquerdo.Pulso, (int)roboAntigo.BracoEsquerdo.Pulso);
                if (!string.IsNullOrWhiteSpace(mensagemPulso))
                    mensagensErro.PulsoEsquerdo = mensagemPulso;

                var mensagemCotovelo = ValidarTransicao<EstadoCotovelo>((int)roboAtualizado.BracoEsquerdo.Cotovelo, (int)roboAntigo.BracoEsquerdo.Cotovelo);
                if (!string.IsNullOrWhiteSpace(mensagemCotovelo))
                    mensagensErro.CotoveloEsquerdo = mensagemCotovelo;

                // • Só poderá movimentar o Pulso caso o Cotovelo esteja Fortemente Contraído.
                if (roboAtualizado.BracoEsquerdo.Pulso != roboAntigo.BracoEsquerdo.Pulso &
                    roboAtualizado.BracoEsquerdo.Cotovelo != EstadoCotovelo.FortementeContraido)
                    mensagensErro.PulsoEsquerdo = "É necessário que o cotovelo esquerdo esteja Fortemente Contraído!";
            }

            // Caso realmente exitir alguma alteração realizamos a validação
            if (roboAtualizado.BracoDireito != roboAntigo.BracoDireito)
            {
                var mensagemPulso = ValidarTransicao<EstadoPulso>((int)roboAtualizado.BracoDireito.Pulso, (int)roboAntigo.BracoDireito.Pulso);
                if (!string.IsNullOrWhiteSpace(mensagemPulso))
                    mensagensErro.PulsoDireito = mensagemPulso;

                var mensagemCotovelo = ValidarTransicao<EstadoCotovelo>((int)roboAtualizado.BracoDireito.Cotovelo, (int)roboAntigo.BracoDireito.Cotovelo);
                if (!string.IsNullOrWhiteSpace(mensagemCotovelo))
                    mensagensErro.CotoveloDireito = mensagemCotovelo;

                //Só poderá movimentar o Pulso caso o Cotovelo esteja Fortemente Contraído.
                if (roboAtualizado.BracoDireito.Pulso != roboAntigo.BracoDireito.Pulso &
                    roboAtualizado.BracoDireito.Cotovelo != EstadoCotovelo.FortementeContraido)
                    mensagensErro.PulsoDireito = "É necessário que o cotovelo direito esteja Fortemente Contraído!";
            }
            #endregion

            #region Cabeça
            // Só validamos caso realmente tenha acontecido alguma alteração
            if (roboAtualizado.Cabeca != roboAntigo.Cabeca)
            {
                var mensagemRotacao = ValidarTransicao<EstadoRotacao>((int)roboAtualizado.Cabeca.Rotacao, (int)roboAntigo.Cabeca.Rotacao);
                if (!string.IsNullOrWhiteSpace(mensagemRotacao))
                    mensagensErro.Rotacao = mensagemRotacao;

                var mensagemInclinacao = ValidarTransicao<EstadoInclinacao>((int)roboAtualizado.Cabeca.Inclinacao, (int)roboAntigo.Cabeca.Inclinacao);
                if (!string.IsNullOrWhiteSpace(mensagemInclinacao))
                    mensagensErro.Inclinacao = mensagemInclinacao;

                //Só poderá Rotacionar a Cabeça caso sua Inclinação da Cabeça não esteja em
                //estado Para Baixo. 
                if (roboAtualizado.Cabeca.Rotacao != roboAntigo.Cabeca.Rotacao &
                    roboAtualizado.Cabeca.Inclinacao == Enums.EstadoInclinacao.ParaBaixo)
                    mensagensErro.Rotacao = "Para modificar a rotação da cabeça é preciso que a inclinação não esteja para baixo!";
            }
            #endregion


            return mensagensErro;
        }

        /// <summary>
        /// Metodo que valida se o usuario não pulou alguma posição
        /// ou passou alguma posição inválida
        /// </summary>
        /// <param name="estadoAtual"></param>
        /// <param name="estadoDesejado"></param>
        /// <returns></returns>
        public static string ValidarTransicao<TEnum>(
            int estadoDesejado,
            int estadoAtual
        ) where TEnum : Enum
        {
            // • Verificação se o estado enviado realmente é uma opção valida
            if (!Enum.IsDefined(typeof(TEnum), estadoDesejado))
            {
                return "Estado Inválido";
            }
            // • Verifica se está sendo pulado alguma estado
            if (Math.Abs(estadoDesejado - estadoAtual) == 1 || Math.Abs(estadoDesejado - estadoAtual) == 0)
            {
                return "";
            }
            else
            {
                return "Estado enviado pulou uma posição!";
            }
        }

        /// <summary>
        /// Função que tem como função verificar se foi encontrado algum erro na validação
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool verificaErros(object obj)
        {
            Type objectType = obj.GetType();
            PropertyInfo[] properties = objectType.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                object value = property.GetValue(obj)!;

                if (value != null && !string.IsNullOrEmpty(value.ToString()) && !value.Equals(false))
                {
                    // Retorna true se encontrar uma propriedade não nula ou não vazia
                    return true;
                }
            }

            // Retorna false se todas as propriedades forem nulas ou vazias
            // ou seja, não possui nenhuma mensagem de erro
            return false;
        }
    }
}