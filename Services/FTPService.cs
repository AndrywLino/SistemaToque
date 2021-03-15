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
            /*string para Rasp do ensino medio*/
            return new FtpClient("192.168.2.99", new System.Net.NetworkCredential { UserName = "pi", Password = "RaspToque" });

            /*string para Rasp do fundamental*/
            //return new FtpClient("192.168.2.98", new System.Net.NetworkCredential { UserName = "pi", Password = "RaspToque" });
        }

        public static async Task UploadFile(string dir = "")
        {
            try
            {
                using (FtpClient ftp = CreateFtpClient())
                {
                    using (FileStream fs = File.OpenRead(dir))
                    {
                        await ftp.UploadAsync(fs, Path.GetFileName(dir));
                    }
                }
            }
            catch (Exception e)
            {
                string error = e.Message;
            }
        }

        public static async Task DownloadFile(string dir, string formato)
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
                            string fileName = ftpItem.Name;
                            string[] typeFile = fileName.Split('.');

                            if (typeFile[1] == formato)
                            {
                                await ftp.DownloadAsync(ms, ftpItem.Name);

                                ms.Position = 0;

                                using (StreamReader sr = new StreamReader(ms))
                                {
                                    string fileContents = await sr.ReadToEndAsync();

                                    if (String.IsNullOrEmpty(fileContents))
                                        throw new Exception("Arquivo Vazio");
                                }

                                await ftp.DownloadFileAsync(Path.Combine(dir, ftpItem.Name), ftpItem.Name);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        public static async Task DownloadFileSpec(string dir, string file)
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
                            string fileName = ftpItem.Name;
                            if (file == fileName)
                            {
                                await ftp.DownloadAsync(ms, ftpItem.Name);

                                ms.Position = 0;

                                using (StreamReader sr = new StreamReader(ms))
                                {
                                    string fileContents = await sr.ReadToEndAsync();

                                    if (String.IsNullOrEmpty(fileContents))
                                        throw new Exception("Arquivo Vazio");
                                }

                                await ftp.DownloadFileAsync(Path.Combine(dir, ftpItem.Name), ftpItem.Name);
                            }

                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        public static async Task DeleteMusic(string dir)
        {
            using (FtpClient ftp = CreateFtpClient())
            {
                try
                {
                    await ftp.DeleteFileAsync(dir);

                }
                catch (Exception e)
                {

                }
            }
        }
    }
}