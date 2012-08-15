using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotService
{
    public interface IRobot
    {
        /// <summary>
        /// Initialize inversion, send settings to Robot
        /// </summary>
        /// <returns>inversionID</returns>
        GuidMessage InitInversion(FileUploadMessage request);

        /// <summary>
        /// Start an inversion
        /// </summary>
        /// <param name="ownerId"> </param>
        /// <param name="inversionId"></param>
        /// <returns></returns>
        bool StartInversion(Guid ownerId, Guid inversionId);

        /// <summary>
        /// Stop  an inversion
        /// </summary>
        /// <param name="ownerId"> </param>
        /// <param name="inversionId"></param>
        /// <returns></returns>
        bool StopInversion(Guid ownerId, Guid inversionId);

        /// <summary>
        /// Query and get all inversions for a well
        /// </summary>
        /// <param name="wellId"></param>
        /// <returns>Dictionary with OwnerID, Inversion pair</returns>
        int QueryInversion(Guid wellId);

        /// <summary>
        /// Retrieve an inversion result which includes both Input and Output files
        /// </summary>
        /// <param name="userId"> </param>
        /// <param name="inversionId"></param>
        /// <param name="accessCode" />
        /// <returns></returns>
        FileDownloadMessage RetrieveInversion(RetrieveMessage retrieveMessage);
    }
}
