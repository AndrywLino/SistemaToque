using CsvHelper;
using SistemaToque.Mappers;
using SistemaToque.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace SistemaToque.Services
{
    public class ServiceCSV
    {
        public static List<ToqueModel> ReadCSVFileToque(string location)
        {
            try
            {
                using (var reader = new StreamReader(location, Encoding.Default))
                using (var csv = new CsvReader(reader))
                {
                    csv.Configuration.RegisterClassMap<ToqueMap>();
                    var records = csv.GetRecords<ToqueModel>().ToList();
                    return records;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}