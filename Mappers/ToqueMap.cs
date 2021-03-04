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
            Map(x => x.IsSegunda).Name("ISSEGUNDA");
            Map(x => x.IsTerca).Name("ISTERCA");
            Map(x => x.IsQuarta).Name("ISQUARTA");
            Map(x => x.IsQuinta).Name("ISQUINTA");
            Map(x => x.IsSexta).Name("ISSEXTA");
            Map(x => x.IsSabado).Name("ISSABADO");
            Map(x => x.IsDomingo).Name("ISDOMINGO");
            Map(x => x.IsAtivo).Name("ISATIVO");
            Map(x => x.NivelEnsino).Name("NIVELENSINO");
        }
    }
}

