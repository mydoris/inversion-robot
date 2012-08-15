using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            request.FileName = "bha.xml";
            request.FileData = new FileStream(@"C:\ForRobot\bha.xml", FileMode.Open);

            Guid ownerId = Guid.NewGuid();
            Console.WriteLine(ownerId.ToString());

            // return inversionId
            Guid inversionId = client.InitInversion(request.FileName, request.FileData);
            Console.WriteLine(inversionId);

            Console.WriteLine(client.StartInversion(ownerId, inversionId));
            Console.WriteLine(client.StopInversion(ownerId, inversionId));
            Console.WriteLine(inversionId.ToString());

            // the returned Filedata from FileDownloadMessage
            FileStream targetStream = null;
            string filePath = Path.Combine(@"C:\ForRobot\", "TRY");
            //using (targetStream = new FileStream(filePath, FileMode.Create))
            //{
                Stream outstream = new FileStream(filePath, FileMode.Create);
                Console.WriteLine(client.RetrieveInversion("accessCode", inversionId, ownerId, out outstream));
                //outstream.CopyTo((targetStream));
                //targetStream.Close();
            outstream.Close();
            
            
    

            //Console.WriteLine("Destination length: {0}", outstream.Length.ToString());
            //using (FileStream source = File.Open(@"c:\data.dat", FileMode.Open))
            //{
            //    outstream.CopyTo(source);
            //}
            //Console.WriteLine("Destination length: {0}", outstream.Length.ToString());

            // Always close the client.
            client.Close();

            Console.ReadLine();

        }
    }
}
