using System;

namespace RobotService
{
    static public class InversionFactory
    {
        public static Inversion CreateInversion()
        {
            Inversion inversion = new Inversion();
            return inversion;
        }

        public static Inversion CreateInversion(Guid ownerId, FileUploadMessage request)
        {
            Inversion inversion = new Inversion(ownerId, request);
            return inversion;
        }
    }
}