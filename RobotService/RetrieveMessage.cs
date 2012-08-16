using System;
using System.ServiceModel;

namespace RobotService
{
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
}