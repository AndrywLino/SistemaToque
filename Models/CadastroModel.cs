using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaToque.Models
{
    public class CadastroModel
    {
        public IEnumerable<HttpPostedFileBase> fileupload { get; set; }
        public string Arquivo { get; set; }
        public string Nome { get; set; }
        public string Hora { get; set; }
        public int Ensino { get; set; }
        public bool IsSegunda { get; set; }
        public bool IsTerca { get; set; }
        public bool IsQuarta { get; set; }
        public bool IsQuinta { get; set; }
        public bool IsSexta { get; set; }
        public bool IsSabado { get; set; }
        public bool IsDomingo { get; set; }
    }
}