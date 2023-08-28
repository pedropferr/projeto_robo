using api.Enums;

namespace api.Models
{ 
    public class Cabeca
    {
        public EstadoRotacao Rotacao { get; set; } = EstadoRotacao.EmRepouso;
        public EstadoInclinacao Inclinacao { get; set; } = EstadoInclinacao.EmRepouso;
    }
}