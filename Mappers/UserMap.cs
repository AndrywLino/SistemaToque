using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaToque.Mappers
{
    public sealed class UserMap : ClassMap<Models.UserModel>
    {
        public UserMap()
        {
            Map(x => x.UserName).Name("UserName");
            Map(x => x.Password).Name("Password");
            Map(x => x.Status).Name("Status");
        }
    }
}