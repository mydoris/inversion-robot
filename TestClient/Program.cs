﻿using System;
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
            request.FileName = "InversionSettings.zip";
            request.FileData = new FileStream(@"C:\InversionSettings.zip", FileMode.Open);

            Guid ownerId = Guid.NewGuid();
            Console.WriteLine("The ownerId is" + ownerId.ToString());

            // return inversionId
            Guid inversionId = client.InitInversion(request.FileName, request.FileData);

            Console.WriteLine("The inversionId is " + inversionId);
            //Console.WriteLine("StartInversion = " + client.StartInversion(ownerId, inversionId));
            Console.WriteLine("StopInversion = " + client.StopInversion(ownerId, inversionId));
            Console.WriteLine("The inversionId is " + inversionId);

            // the returned Filedata from FileDownloadMessage
            //string filePath = Path.Combine(@"C:\ForRobot\", "TRY");
            //Stream myStream = new FileStream(filePath, FileMode.Create);
            Stream outstream;
            Console.WriteLine(client.RetrieveInversion("accessCode", Guid.Parse("fcda73db-98f6-4fdf-b599-6dff22be3285"), ownerId, out outstream));
            //outstream.CopyTo(myStream);

            //Console.WriteLine("Destination length: {0}", myStream.Length);
            ////Console.WriteLine("Destination length: {0}", outstream.Length);
            //myStream.Close();
            //outstream.Close();

            //Always close the client.
            client.Close();

            Console.ReadLine();

        }
    }
}
