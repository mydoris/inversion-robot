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

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public Guid InitInversion(Guid ownerId, List<InversionFile> inversionFiles)
        {

            //return ownerId;
            return _robot.InitInversion(ownerId, inversionFiles);
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

        public byte[] RetrieveInversion(Guid userId, Guid inversionId, string accessCode)
        {
            return _robot.RetrieveInversion(userId, inversionId, accessCode);
        }
    }
}
