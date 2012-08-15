using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TestClient.RobotServiceReference;


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
            
            // Always close the client.
            client.Close();

            Console.ReadLine();

        }
    }
}
