using System;
using System.ServiceModel;

namespace RobotService
{
    [MessageContract]
    public class GuidMessage
    {
        [MessageBodyMember]
        public Guid InversionId;
    }
}