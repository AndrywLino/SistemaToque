using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaToque.Models
{
    public class ToqueExportModel
    {
        public string Arquivo { get; set; }
        public string Nome { get; set; }
        public string Hora { get; set; }
        public int Canal { get; set; }
        public bool IsAtivo { get; set; }
        public int NivelEnsino { get; set; }
        public string DiasSemana { get; set; }
        public string UltimoToque { get; set; }

    }
}