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
            Map(x => x.Arquivo).Name("Arquivo");
            Map(x => x.Nome).Name("Nome");
            Map(x => x.Hora).Name("Hora");
            Map(x => x.Canal).Name("Canal");
            Map(x => x.IsSegunda).Name("IsSegunda");
            Map(x => x.IsTerca).Name("IsTerca");
            Map(x => x.IsQuarta).Name("IsQuarta");
            Map(x => x.IsQuinta).Name("IsQuinta");
            Map(x => x.IsSexta).Name("IsSexta");
            Map(x => x.IsSabado).Name("IsSabado");
            Map(x => x.IsDomingo).Name("IsDomingo");
            Map(x => x.IsAtivo).Name("IsAtivo");
            Map(x => x.NivelEnsino).Name("NivelEnsino");
            Map(x => x.UltimoToque).Name("UltimoToque");
            Map(x => x.StartSegs).Name("StartSegs");
        }
    }
}

