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

        public static int WriteCSVFileToque(string path, List<ToqueExportModel> toque)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path, false, new UTF8Encoding(true)))
                using (CsvWriter cw = new CsvWriter(sw))
                {
                    cw.WriteHeader<ToqueExportModel>();
                    cw.NextRecord();
                foreach (ToqueExportModel toq in toque)
                    {
                        cw.WriteRecord<ToqueExportModel>(toq);
                        cw.NextRecord();
                    }
                }
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }

        }
    }
}