using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RobotService
{
    public class Robot : IRobot
    {
        private static List<Inversion> _inversions;

        //user-inversion Dictionary
        private static Dictionary<Guid, Inversion> _userInversionLookup;

        static Robot()
        {
            Inversions = new List<Inversion>();
            UserInversionLookup = new Dictionary<Guid, Inversion>();
        }

        public static List<Inversion> Inversions
        {
            get { return _inversions; }
            set { _inversions = value; }
        }

        public static Dictionary<Guid, Inversion> UserInversionLookup
        {
            get { return _userInversionLookup; }
            set { _userInversionLookup = value; }
        }

        public Guid InitInversion(Guid ownerId, List<InversionFile> inversionFiles)
        {
            var inversion = InversionFactory.CreateInversion(ownerId, inversionFiles);
            // Add into user-inversion Dictionary
            UserInversionLookup.Add(ownerId, inversion);
            // Add into inversion list
            Inversions.Add(inversion);
            // save the files into input folder
            inversion.Init();
            return inversion.InversionId;
        }

        public bool StartInversion(Guid ownerId, Guid inversionId)
        {
            Inversion inversion = null;
            foreach (var inv in Inversions.Where(inv => inv.InversionId.Equals(inversionId)))
            {
                inversion = inv;
            }
            return inversion != null && inversion.Start();
        }

        public bool StopInversion(Guid ownerId, Guid inversionId)
        {
            Inversion inversion = null;
            foreach (var inv in Inversions)
            {
                if (inv.InversionId.Equals(inversionId))
                {
                    inversion = inv;
                }
            }
            return inversion != null && inversion.Stop();
        }

        public int QueryInversion(Guid wellId)
        {
            var inversionQuery = from inversion in Inversions
                                 where inversion.WellId.Equals(wellId)
                                 select inversion;
            //We can return an inversion list here.
            //var inversionList = inversionQuery.ToList();

            return inversionQuery.Count();
        }

        public byte[] RetrieveInversion(Guid userId, Guid inversionId, string accessCode)
        {
            Inversion inversion = null;
            var inversionQuery = from inv in Inversions
                                 where inv.InversionId.Equals(inversionId)
                                 select inv;
            foreach (var inv in inversionQuery)
            {
                inversion = inv;
            }

            if (inversion != null && !inversion.CheckAccessCode(accessCode))
            {
                return null;
            }

            // Add user who can access the inversion into user-inversion dictionary
            UserInversionLookup.Add(userId, inversion);

            return inversion.RetrieveFiles();
        }

        public List<Guid> GetUsersByInversionId(Guid inversionId)
        {
            var users = new List<Guid>();

            var usersQuery = from user in UserInversionLookup
                             where user.Value.InversionId.Equals(inversionId)
                             select user;

            foreach (var keyValuePair in usersQuery)
            {
                users.Add(keyValuePair.Key);
            }

            return users;
        }
        
    }

    public class InversionFactory
    {
        public static Inversion CreateInversion()
        {
            Inversion inversion = new Inversion();
            return inversion;
        }

        public static Inversion CreateInversion(Guid ownerId, List<InversionFile> inversionFiles)
        {
            Inversion inversion = new Inversion(ownerId, inversionFiles);
            return inversion;
        }
    }
}