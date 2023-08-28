namespace api.Models
{
    public class Robo
    {
        // Construtor que inicializa a posição do robo
        public Robo()
        {
            BracoEsquerdo = new Braco();
            BracoDireito = new Braco();
            Cabeca = new Cabeca();
        }

        public Robo(Braco bracoEsquerdo, Braco bracoDireito, Cabeca cabeca)
        {
            BracoEsquerdo = bracoEsquerdo;
            BracoDireito = bracoDireito;
            Cabeca = cabeca;
        }

        public Braco BracoEsquerdo { get; set; }
        public Braco BracoDireito { get; set; }
        public Cabeca Cabeca { get; set; }
    }
}