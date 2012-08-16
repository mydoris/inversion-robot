using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace RobotService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RobotService" in code, svc and config file together.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class RobotService : IRobotService
    {
        private static IRobot _robot;

        public RobotService()
        {
            _robot = new Robot();
        }

        public RobotService(IRobot robot)
        {
            _robot = robot;
        }

        public GuidMessage InitInversion(FileUploadMessage request)
        {
            return _robot.InitInversion(request);
        }

        public bool StartInversion(Guid ownerId, Guid inversionId)
        {
            return _robot.StartInversion(ownerId, inversionId);
        }

        public bool StopInversion(Guid ownerId, Guid inversionId)
        {
            return _robot.StopInversion(ownerId, inversionId);
        }

        public int QueryInversion(Guid wellId)
        {
            return _robot.QueryInversion(wellId);
        }

        public FileDownloadMessage RetrieveInversion(RetrieveMessage requestrRetrieveMessage)
        {
            return _robot.RetrieveInversion(requestrRetrieveMessage);
        }
    }
}
