using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUnitOfWorkCRUD.Core.DomainServices;
using TestUnitOfWorkCRUD.Core.Entities;

namespace TestUnitOfWorkCRUD.Infrastructure.Repositories
{
    public class GlobalUnitOfWorkRepository : IGlobalUnitOfWorkRepository
    {
        public async void CreateRequestResponseLogs(string description, User user, int result, string response)
        {
            try
            {
                var docPath = AppDomain.CurrentDomain.BaseDirectory;

                var pathToData = Path.GetFullPath(Path.Combine(
                        docPath ?? throw new InvalidOperationException("Path value is Null"), "Request and Response Log"));

                if (!Directory.Exists(pathToData))
                {
                    Directory.CreateDirectory(pathToData);
                }
                var path = Path.Combine(pathToData, DateTime.Now.ToString("dd-MM-yy") + ".txt");

                // Write the specified text asynchronously to a new file named "WriteTextAsync.txt".
                var outputFile = new StreamWriter(path, true);
                outputFile.Close();

                var sReader = new StreamReader(path);
                var fileContent = await sReader.ReadToEndAsync();
                if (fileContent.Length <= 0)
                {
                    sReader.Close();
                    await using var wr = new StreamWriter(path, true);
                    await wr.WriteAsync(
                        "---------------------------------------------------------------------------------- \n" +
                        "Application Request Response log Info File.(c)Copyright 2023 TNK Technologies \n" +
                        "This File was Generated for Developers Purposes, Please DO NOT DELETE! \n" +
                        "Log Generated On : " + DateTime.Now + "\n" +
                        "---------------------------------------------------------------------------------- \n" +
                        "Date & time - " + DateTime.Now + "\n"
                        + "Description - " + description + "\n"
                        + "User Email - " + user.Email + "\n"
                        + "Result     - " + result + "\n"
                        + "Response    - " + response + "\n"
                        + "------------------------------------------------------------------------------- end \n\n");
                }
                else
                {
                    sReader.Close();
                    await using var wr = new StreamWriter(path, true);
                    await wr.WriteAsync(
                        "---------------------------------------------------------------------------------- \n" +
                        "CWT Request Response log Info File.(c)Copyright 2022  Technologies \n" +
                        "This File was Generated for Developers Purposes, Please DO NOT DELETE! \n" +
                        "Log Generated On : " + DateTime.Now + "\n" +
                        "---------------------------------------------------------------------------------- \n" +
                        "Date & time - " + DateTime.Now + "\n"
                        + "Description - " + description + "\n"
                        + "User Email - " + user.Email + "\n"
                        + "Result     - " + result + "\n"
                        + "Response    - " + response + "\n"
                        + "------------------------------------------------------------------------------- end \n\n");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
    }
}
