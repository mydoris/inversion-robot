using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace RobotService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IRobotService" in both code and config file together.
    [ServiceContract]
    public interface IRobotService
    {
        // TODO: Add your service operations here

        /// <summary>
        /// Initialize inversion, send settings to RobotService (Upload 3 files to RobotService)
        /// </summary>
        /// <returns>inversionID</returns>
        [OperationContract]
        GuidMessage InitInversion(FileUploadMessage request);

        /// <summary>
        /// Start an inversion
        /// </summary>
        /// <param name="ownerId"> </param>
        /// <param name="inversionId"></param>
        /// <returns></returns>
        [OperationContract]
        bool StartInversion(Guid ownerId, Guid inversionId);

        /// <summary>
        /// Stop  an inversion
        /// </summary>
        /// <param name="ownerId"> </param>
        /// <param name="inversionId"></param>
        /// <returns></returns>
        [OperationContract]
        bool StopInversion(Guid ownerId, Guid inversionId);

        /// <summary>
        /// Query and get all inversions for a well
        /// </summary>
        /// <param name="wellId"></param>
        /// <returns>Dictionary with OwnerID, Inversion pair</returns> 
        [OperationContract]
        int QueryInversion(Guid wellId);

        /// <summary>
        /// Retrieve an inversion result which includes both Input and Output files
        /// return what?
        /// </summary>
        /// <param name="userId"> </param>
        /// <param name="inversionId"></param>
        /// <param name="accessCode"></param>
        /// <returns></returns>
        [OperationContract]
        FileDownloadMessage RetrieveInversion(RetrieveMessage retrieveMessage);

        [OperationContract]
        string GetData(int value);

        //Inversion GetInversion(Guid userId, Guid inversionId, string accessCode);
        //List<Inversion> GetInversionList(Guid userId, Guid inversionId, string accessCode);

        //[OperationContract]
        //CompositeType GetDataUsingDataContract(CompositeType composite);
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
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

    [MessageContract]
    public class FileDownloadMessage
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



    [MessageContract]
    public class GuidMessage
    {
        [MessageBodyMember]
        public Guid InversionId;
    }

    [MessageContract]
    public class RetrieveMessage
    {
        [MessageBodyMember]
        public Guid UserId;

        [MessageBodyMember]
        public Guid InversionId;

        [MessageBodyMember]
        public string AccessCode;
    }



    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    //[DataContract]
    //public class CompositeType
    //{
    //    bool boolValue = true;
    //    string stringValue = "Hello ";

    //    [DataMember]
    //    public bool BoolValue
    //    {
    //        get { return boolValue; }
    //        set { boolValue = value; }
    //    }

    //    [DataMember]
    //    public string StringValue
    //    {
    //        get { return stringValue; }
    //        set { stringValue = value; }
    //    }
    //}
}
