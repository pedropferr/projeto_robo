namespace api.Models
{   
    /// <summary>
    /// Objeto que representa os erros que serão exibidos na tela
    /// </summary>
    public class MensagensErroRobo {

        public MensagensErroRobo() {

            CotoveloEsquerdo = "";
            PulsoEsquerdo = "";
            CotoveloDireito = "";
            PulsoDireito = "";
            Rotacao = "";
            Inclinacao = "";
            PossuiErros = false;
        }

        public string CotoveloEsquerdo { get; set; }
        public string PulsoEsquerdo { get; set; }
        public string CotoveloDireito { get; set; }
        public string PulsoDireito { get; set; }
        public string Rotacao { get; set; }
        public string Inclinacao { get; set; }
        public bool PossuiErros { get; set; }
    }
}