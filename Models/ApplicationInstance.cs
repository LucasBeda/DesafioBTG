using System.Collections.Concurrent;

namespace DesafioBTG.Models
{
    public class ApplicationInstance
    {
        public Cotacao cotacao { get; set; } = new Cotacao();
        public int posicaoGrafico { get; set; } = 0;
    }
}
