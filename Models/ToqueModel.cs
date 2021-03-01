using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaToque.Models
{
    public class ToqueModel
    {
        public string Arquivo { get; set; }
        public string Hora { get; set; }
        public int Canal { get; set; }
        public string DiaSemana { get; set; }
        public int IsAtivo { get; set; }
        public int NivelEnsino { get; set; }
    }
}