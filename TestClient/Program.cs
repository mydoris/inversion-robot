using System;
using System.Collections.Generic;
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
            var inversionFiles = new List<InversionFile>();
            var inversionFile = new InversionFile();

            inversionFile.FileName = "Sample Name";
            inversionFiles.Add(inversionFile);

            Guid ownerId = Guid.NewGuid();
            Console.WriteLine(ownerId.ToString());

            // return inversionId
            Guid inversionId = client.InitInversion(ownerId, inversionFiles);
            Console.WriteLine(inversionId);
            
            Console.WriteLine(client.StartInversion(ownerId, inversionId));
            Console.WriteLine(client.StopInversion(ownerId, inversionId));

            // Always close the client.
            client.Close();

            Console.ReadLine();

        }
    }
}
