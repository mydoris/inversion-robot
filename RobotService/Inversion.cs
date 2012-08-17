using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using Ionic.Zip;

namespace RobotService
{
    public class Inversion
    {
        private Guid _inversionId;
        private string _name;
        private Guid _ownerId;
        private FileUploadMessage _request;
        private readonly string _accessCode;
        private Guid _wellId = Guid.Parse("af5e2870-7891-4bec-ae7a-6e9ce5e0bf2c");

        public Inversion(Guid ownerId, FileUploadMessage request)
        {
            _inversionId = Guid.NewGuid();

            // TODO how to define the name ???
            _name = InversionId.ToString();
            _ownerId = ownerId;
            _request = request;

            // TODO how to generate the accessCode
            _accessCode = Guid.NewGuid().ToString();

            // TODO
            _wellId = GetWellId(request);
        }

        public Inversion()
        {
            throw new NotImplementedException();
        }

        private Guid GetWellId(FileUploadMessage request)
        {
            return _wellId;
        }

        public Guid InversionId
        {
            get { return _inversionId; }
            set { _inversionId = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public Guid OwnerId
        {
            get { return _ownerId; }
            set { _ownerId = value; }
        }

        public Guid WellId
        {
            get { return _wellId; }
            set { _wellId = value; }
        }

        public void Init()
        {
            init(_request);
        }

        private void init(FileUploadMessage request)
        {
            string fileName = request.FileName;
            Stream sourceStream = request.FileData;
            // TODO How to get well Id ?????????????
            _wellId = GetWellId(request);
            SaveStreamToFile(fileName, sourceStream);
        }

        private void SaveStreamToFile(string fileName, Stream sourceStream)
        {
            string uploadFolder = @"c:\Inversions\";

            // TODO how to get well ID
            string wellIdFolder = _wellId + @"\";
            string inversionIdFolder = _inversionId + @"\";

            // do not put request files into input Folder, but its upper level folder 
            uploadFolder = uploadFolder + wellIdFolder + inversionIdFolder;

            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
                Directory.CreateDirectory(uploadFolder + @"Input\");
                Directory.CreateDirectory(uploadFolder + @"Output\");
            }

            string filePath = Path.Combine(uploadFolder, fileName);

            FileStream targetStream = null;
            if (!sourceStream.CanRead)
            {
                throw new Exception("Can't read!");
            }

            using (targetStream = new FileStream(filePath, FileMode.Create))
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
                sourceStream.CopyTo(targetStream);
                targetStream.Close();
                sourceStream.Close();
            }

            using (ZipFile zip = ZipFile.Read(filePath))
            {
                foreach (ZipEntry e in zip)
                {
                    e.Extract(uploadFolder, ExtractExistingFileAction.OverwriteSilently);  // overwrite == true
                }
            }
        }

        public Stream Retrieve()
        {
            string downloadFolder = @"c:\Inversions\";
            string wellIdFolder = _wellId + @"\";
            string inversionIdFolder = _inversionId + @"\";
            downloadFolder = downloadFolder + wellIdFolder + inversionIdFolder + @"Output\";

            // Generate 100 files for test
            for (int i = 0; i < 100; i++)
            {
                GenerateRandomFilesInDirectory(downloadFolder);
            }
            
            Stream targetStream = new MemoryStream();
            
            using (ZipFile zip = new ZipFile())
            {
                string[] files = Directory.GetFiles(downloadFolder);
                zip.AddFiles(files, "");
                zip.Comment = "This zip was created at " + System.DateTime.Now.ToString("G");
                //zip.Save(targetStream);
                zip.Save(@"C:\OutputTest.zip");
            }
            
            Stream fileStream = new FileStream(@"C:\OutputTest.zip", FileMode.Open, FileAccess.Read);

            //using (ZipFile zip = new ZipFile())
            //{
            //    string[] files = Directory.GetFiles(downloadFolder);
            //    zip.AddFiles(files, "");
            //    zip.Comment = "This zip was created at " + System.DateTime.Now.ToString("G");
            //    zip.Save(targetStream);
            //    //zip.Save(@"C:\ForRobot.zip");
            //}
            //Console.WriteLine("target stream from Retrieve method" + targetStream.Length);

            return fileStream;
        }

        private static void GenerateRandomFilesInDirectory(string downloadFolder)
        {
            string activeDir = downloadFolder;
            //Create a new subfolder under the current active folder 
            //string newPath = System.IO.Path.Combine(activeDir, "Output");
            string newFileName = System.IO.Path.GetRandomFileName();

            // Combine the new file name with the path
            activeDir = System.IO.Path.Combine(activeDir, newFileName);

            // Create the file and write to it. 
            // DANGER: System.IO.File.Create will overwrite the file 
            // if it already exists. This can occur even with 
            // random file names. 
            if (!System.IO.File.Exists(activeDir))
            {
                using (System.IO.FileStream fs = System.IO.File.Create(activeDir))
                {
                    for (byte i = 0; i < 100; i++)
                    {
                        fs.WriteByte(i);
                    }
                }
            }
        }

        public bool Start()
        {
            return true;
            //throw new NotImplementedException();
        }

        public bool Stop()
        {
            return true;
            //throw new NotImplementedException();
        }

        public bool CheckAccessCode(string accessCode)
        {
            return true;
            //throw new NotImplementedException();
        }



    }
}
