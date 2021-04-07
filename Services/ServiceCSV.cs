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
        public static List<ToqueModel> ReadCSVFileToque(string path)
        {
            try
            {
                using (var reader = new StreamReader(path, Encoding.UTF8))
                using (var csv = new CsvReader(reader))
                {
                    csv.Configuration.RegisterClassMap<ToqueMap>();
                    var records = csv.GetRecords<ToqueExportModel>().ToList();
                    List<ToqueModel> retorno = new List<ToqueModel>();

                    foreach (ToqueExportModel item in records)
                    {
                        ToqueModel it = new ToqueModel();

                        it.Arquivo = item.Arquivo;
                        it.Nome = item.Nome;
                        it.Hora = item.Hora;
                        it.Canal = item.Canal;
                        it.IsAtivo = item.IsAtivo;
                        it.UltimoToque = item.UltimoToque;
                        //continuar com concatenação do diasemana em records
                        string[] diasSemana = item.DiasSemana.Split(',');
                        foreach (string dia in diasSemana)
                        {
                            if (dia == "0")
                                it.IsDomingo = true;
                            if (dia == "1")
                                it.IsSegunda = true;
                            if (dia == "2")
                                it.IsTerca = true;
                            if (dia == "3")
                                it.IsQuarta = true;
                            if (dia == "4")
                                it.IsQuinta = true;
                            if (dia == "5")
                                it.IsSexta = true;
                            if (dia == "6")
                                it.IsSabado = true;
                        }
                        it.NivelEnsino = item.NivelEnsino;
                        retorno.Add(it);

                    }
                    return retorno;
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

        public static int WriteCSVFileLogin(string path, List<UserModel> users)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path, false, new UTF8Encoding(true)))
                using (CsvWriter cw = new CsvWriter(sw))
                {
                    cw.WriteHeader<UserModel>();
                    cw.NextRecord();
                    foreach (UserModel user in users)
                    {
                        cw.WriteRecord(user);
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

        public static List<UserModel> ReadCSVFileLogin(string path)
        {
            try
            {
                using (var reader = new StreamReader(path, Encoding.Default))
                using (var csv = new CsvReader(reader))
                {
                    csv.Configuration.RegisterClassMap<UserMap>();
                    var records = csv.GetRecords<UserModel>().ToList();
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