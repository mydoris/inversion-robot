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
            _inversions = new List<Inversion>();
            _userInversionLookup = new Dictionary<Guid, Inversion>();
        }

        public Guid InitInversion(Guid ownerId, List<InversionFile> inversionFiles)
        {
            var inversion = InversionFactory.CreateInversion(ownerId, inversionFiles);
            // Add into inversion list
            _userInversionLookup.Add(ownerId, inversion);
            
            _inversions.Add(inversion);
            // Add into user-inversion Dictionary
            //_userInversionLookup.Add(ownerId, inversion);

            return inversion.InversionId;
        }

        public bool StartInversion(Guid ownerId, Guid inversionId)
        {
            Inversion inversion = null;
            foreach (var inv in _inversions.Where(inv => inv.InversionId.Equals(inversionId)))
            {
                inversion = inv;
            }
            return inversion != null && inversion.Start();
        }

        public bool StopInversion(Guid ownerId, Guid inversionId)
        {
            Inversion inversion = null;
            foreach (var inv in _inversions)
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
            var inversionQuery = from inversion in _inversions
                                 where inversion.WellId.Equals(wellId)
                                 select inversion;
            //We can return an inversion list here.
            //var inversionList = inversionQuery.ToList();

            return inversionQuery.Count();
        }

        public byte[] RetrieveInversion(Guid userId, Guid inversionId, string accessCode)
        {
            Inversion inversion = null;
            var inversionQuery = from inv in _inversions
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
            _userInversionLookup.Add(userId, inversion);

            return inversion.RetrieveFiles();
        }

        public List<Guid> GetUsersByInversionId(Guid inversionId)
        {
            var users = new List<Guid>();

            var usersQuery = from user in _userInversionLookup
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