using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotService
{
    public interface IRobot
    {
        GuidMessage InitInversion(FileUploadMessage request);

        bool StartInversion(Guid ownerId, Guid inversionId);

        bool StopInversion(Guid ownerId, Guid inversionId);

        int QueryInversion(Guid wellId);

        FileDownloadMessage RetrieveInversion(RetrieveMessage retrieveMessage);
    }
}
