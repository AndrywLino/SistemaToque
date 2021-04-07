using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CsvHelper.Configuration;

namespace SistemaToque.Mappers
{
    public sealed class ToqueMap : ClassMap<Models.ToqueExportModel>
    {
        public ToqueMap()
        {
            Map(x => x.Arquivo).Name("Arquivo");
            Map(x => x.Nome).Name("Nome");
            Map(x => x.Hora).Name("Hora");
            Map(x => x.Canal).Name("Canal");
            Map(x => x.DiasSemana).Name("DiasSemana");
            Map(x => x.IsAtivo).Name("IsAtivo");
            Map(x => x.NivelEnsino).Name("NivelEnsino");
            Map(x => x.UltimoToque).Name("UltimoToque");
            Map(x => x.StartSegs).Name("StartSegs");
        }
    }
}

