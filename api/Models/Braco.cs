using api.Enums;

namespace api.Models
{    
    public class Braco
    {
        public EstadoCotovelo Cotovelo { get; set; } = EstadoCotovelo.EmRepouso;
        public EstadoPulso Pulso { get; set; } = EstadoPulso.EmRepouso;
    }
}