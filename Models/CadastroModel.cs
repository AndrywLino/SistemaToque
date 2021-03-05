using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaToque.Models
{
    public class CadastroModel
    {
        public IEnumerable<HttpPostedFileBase> Musica { get; set; }
        public string Arquivo { get; set; }
        public string Nome { get; set; }
        public string Hora { get; set; }
        public int Ensino { get; set; }
        public int IsSegunda { get; set; }
        public int IsTerca { get; set; }
        public int IsQuarta { get; set; }
        public int IsQuinta { get; set; }
        public int IsSexta { get; set; }
        public int IsSabado { get; set; }
        public int IsDomingo { get; set; }
    }
}