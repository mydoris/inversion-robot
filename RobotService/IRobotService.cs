using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace RobotService
{
    [ServiceContract]
    public interface IRobotService
    {
        // TODO: Add your service operations here

        [OperationContract]
        GuidMessage InitInversion(FileUploadMessage request);

        [OperationContract]
        bool StartInversion(Guid ownerId, Guid inversionId);

        [OperationContract]
        bool StopInversion(Guid ownerId, Guid inversionId);

        [OperationContract]
        int QueryInversion(Guid wellId);

        [OperationContract]
        FileDownloadMessage RetrieveInversion(RetrieveMessage requestrRetrieveMessage);

        //Inversion GetInversion(Guid userId, Guid inversionId, string accessCode);
        //List<Inversion> GetInversionList(Guid userId, Guid inversionId, string accessCode);
    }
    
}
