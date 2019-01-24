using System.Collections.Generic;

namespace ConfereBaanXDesenho
{
    public class Rack
    {
        public string codigoDoRack { get; set; }
        public List<Componente> ListaDeComponentes { get; set; }

        public Rack()
        {
            ListaDeComponentes = new List<Componente>();
        }
    }

}
