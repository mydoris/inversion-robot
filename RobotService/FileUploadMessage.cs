using System.IO;
using System.ServiceModel;

namespace RobotService
{
    [MessageContract]
    public class FileUploadMessage
    {
        private string _fileName;
        private Stream _fileData;

        [MessageHeader(MustUnderstand = true)]
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        [MessageBodyMember(Order = 1)]
        public Stream FileData
        {
            get { return _fileData; }
            set { _fileData = value; }
        }
    }
}