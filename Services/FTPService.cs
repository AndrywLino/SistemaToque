using FluentFTP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SistemaToque.Services
{
    public class FTPService
    {
        private static FtpClient CreateFtpClient()
        {
            return new FtpClient("192.168.2.102", new System.Net.NetworkCredential { UserName = "pi", Password = "RaspToque" });
        }

        public static async Task UploadFile()
        {
            const string fileToUpload = "C:\\Users\\andrywafonso\\source\\repos\\SistemaToque\\CSV\\toque.csv";

            try
            {
                using (FtpClient ftp = CreateFtpClient())
                {
                    using (FileStream fs = File.OpenRead(fileToUpload))
                    {
                        await ftp.UploadAsync(fs, Path.GetFileName(fileToUpload));
                    }
                }
            }
            catch (Exception e)
            {
                string error = e.Message;
            }
        }

        public static async Task DownloadFile(string dir)
        {
            try
            {
                using (FtpClient ftp = CreateFtpClient())
                {
                    FtpListItem[] listing = await ftp.GetListingAsync();
                    foreach (FtpListItem ftpItem in listing)
                    {
                        if (ftpItem.Type != FtpFileSystemObjectType.File)
                            continue;

                        using (MemoryStream ms = new MemoryStream())
                        {
                            await ftp.DownloadAsync(ms, ftpItem.Name);

                            ms.Position = 0;

                            using (StreamReader sr = new StreamReader(ms))
                            {
                                string fileContents = await sr.ReadToEndAsync();

                                if (String.IsNullOrEmpty(fileContents))
                                    throw new Exception("Arquivo Vazio");
                            }
                            string dir1 = Path.Combine(dir, ftpItem.Name);
                            await ftp.MoveFileAsync(ftpItem.Name, dir1);
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
        }
    }
}