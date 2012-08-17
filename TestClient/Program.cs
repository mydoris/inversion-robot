using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using RobotService;
using TestClient.RobotServiceReference;
using FileDownloadMessage = RobotService.FileDownloadMessage;
using FileUploadMessage = RobotService.FileUploadMessage;
using RetrieveMessage = RobotService.RetrieveMessage;


namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            RobotServiceClient client = new RobotServiceClient();

            // Use the 'client' variable to call operations on the service.

            FileUploadMessage request = new FileUploadMessage();
            request.FileName = "InversionSettings.zip";
            request.FileData = new FileStream(@"C:\InversionSettings.zip", FileMode.Open);

            Guid ownerId = Guid.NewGuid();
            Console.WriteLine("The ownerId is" + ownerId.ToString());

            // return inversionId
            Guid inversionId = client.InitInversion(request.FileName, request.FileData);

            Console.WriteLine("The inversionId is " + inversionId);
            Console.WriteLine("StartInversion = " + client.StartInversion(ownerId, inversionId));
            Console.WriteLine("StopInversion = " + client.StopInversion(ownerId, inversionId));
            Console.WriteLine("The inversionId is " + inversionId);

            // the returned Filedata from FileDownloadMessage
            string filePath = Path.Combine(@"C:\", "InversionResult.zip");
            Stream myStream;
            Stream outstream;
            Console.WriteLine(client.RetrieveInversion("accessCode", inversionId, ownerId, out outstream));

            using (myStream = new FileStream(filePath, FileMode.Create))
            {
                ////read from the input stream in 4K chunks
                ////and save to output stream
                //const int bufferLen = 4096;
                //byte[] buffer = new byte[bufferLen];
                //int count = 0;
                //while ((count = sourceStream.Read(buffer, 0, bufferLen)) > 0)
                //{
                //    targetStream.Write(buffer, 0, count);
                //}
                outstream.CopyTo(myStream);
                outstream.Close();
                myStream.Close();
            }

            Console.WriteLine("Returned outstream: {0}", outstream.Length);
            Console.WriteLine("Returned myStream: {0}", myStream.Length);

            //Always close the client.
            client.Close();

            Console.ReadLine();

        }
    }
}
