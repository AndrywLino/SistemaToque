using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CsvHelper.Configuration;

namespace SistemaToque.Mappers
{
    public sealed class ToqueMap : ClassMap<Models.ToqueModel>
    {
        public ToqueMap()
        {
            Map(x => x.Arquivo).Name("ARQUIVO");
            Map(x => x.Hora).Name("HORA");
            Map(x => x.Canal).Name("CANAL");
            Map(x => x.DiaSemana).Name("DIASEMANA");
            Map(x => x.IsAtivo).Name("ISATIVO");
            Map(x => x.NivelEnsino).Name("NIVELENSINO");
        }
    }
}

