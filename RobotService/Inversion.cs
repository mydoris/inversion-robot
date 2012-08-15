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
        private Guid _wellId;

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
            return Guid.NewGuid();
            //throw new NotImplementedException();
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

        private void init(FileUploadMessage request )
        {
            string uploadFolder = @"c:\Inversions\";

            string fileName = request.FileName;
            Stream sourceStream = request.FileData;

            // TODO how to get well ID
            //string wellId = GetWellId(request).ToString();
            string wellId = Guid.NewGuid().ToString();
            string wellIdFolder = wellId + @"\";
            string inversionIdFolder = InversionId + @"\";
            //string dateString = DateTime.Now.ToShortDateString() + @"\";
                
            FileStream targetStream = null;

            if (!sourceStream.CanRead)
            {
                throw new Exception("Can't read!");
            }
            
            // do not put request files into input Folder, but its upper level folder 
            uploadFolder = uploadFolder + wellIdFolder + inversionIdFolder;

            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }
            
            string filePath = Path.Combine(uploadFolder, fileName);

            using (targetStream = new FileStream(filePath, FileMode.Create))
            {
                //read from the input stream in 4K chunks
                //and save to output stream
                const int bufferLen = 4096;
                byte[] buffer = new byte[bufferLen];
                int count = 0;
                while ((count = sourceStream.Read(buffer, 0, bufferLen)) > 0)
                {
                    targetStream.Write(buffer, 0, count);
                }

                //sourceStream.CopyTo(targetStream);
                targetStream.Close();
                sourceStream.Close();
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


        public Stream Retrieve()
        {
            Stream targetStream = null;
            string downloadFolder = @"c:\Inversions\";
            string wellId = "5b1f5ed5-5bf1-4b52-92fa-b83ff1476ffd";
            string wellIdFolder = wellId + @"\";
            string inversionIdFolder = InversionId + @"\";

            downloadFolder = downloadFolder + wellIdFolder + inversionIdFolder + @"Output";

            using (ZipFile zip = new ZipFile())
            {
                string[] files = Directory.GetFiles(@"C:\temp");
                // add all those files to the ProjectX folder in the zip file
                zip.AddFiles(files, "temp");
                zip.Comment = "This zip was created at " + System.DateTime.Now.ToString("G");
                zip.Save(@"c:\inversionId.zip");

                //string[] files = Directory.GetFiles(@"C:\Inversions\af5e2870-7891-4bec-ae7a-6e9ce5e0bf2c\5b1f5ed5-5bf1-4b52-92fa-b83ff1476ffd\Output");
                //// add all those files to the ProjectX folder in the zip file
                //zip.AddFiles(files, "temp");
                //zip.Comment = "This zip was created at " + System.DateTime.Now.ToString("G");
                //zip.Save("inversionId.zip");
                ////zip.Save(outputStream);
            }

            return targetStream;

        }
    }
}
