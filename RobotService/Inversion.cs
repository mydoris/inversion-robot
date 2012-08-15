using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotService
{
    public class Inversion
    {
        private Guid _inversionId;
        private string _name;
        private Guid _ownerId;
        private List<InversionFile > _inversionFiles;
        private readonly string _accessCode;
        private Guid _wellId;

        public Inversion(Guid ownerId, List<InversionFile> inversionFiles)
        {
            _inversionId = Guid.NewGuid();

            // TODO how to define the name ???
            _name = ownerId.ToString() + InversionId.ToString();
            _ownerId = ownerId;
            _inversionFiles = inversionFiles;

            // TODO how to generate the accessCode
            _accessCode = Guid.NewGuid().ToString();

            // TODO
            _wellId = GetWellId(inversionFiles);
        }

        public Inversion()
        {
            throw new NotImplementedException();
        }

        private Guid GetWellId(List<InversionFile> inversionFiles)
        {
            return Guid.NewGuid();
            //throw new NotImplementedException();
        }

        public Guid InversionId
        {
            get { return _inversionId; }
            set { _inversionId = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public Guid OwnerId
        {
            get { return _ownerId; }
            set { _ownerId = value; }
        }

        public Guid WellId
        {
            get { return _wellId; }
            set { _wellId = value; }
        }

        public bool Start()
        {
            return true;
            //throw new NotImplementedException();
        }

        public bool Stop()
        {
            return true;
            //throw new NotImplementedException();
        }

        public bool CheckAccessCode(string accessCode)
        {
            throw new NotImplementedException();
        }

        public byte[] RetrieveFiles()
        {
            throw new NotImplementedException();
        }
    }
}
