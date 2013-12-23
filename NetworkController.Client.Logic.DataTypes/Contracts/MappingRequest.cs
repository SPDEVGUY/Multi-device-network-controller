using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkController.Client.Logic.DataTypes.Contracts
{
    public class MappingRequest
    {
        public string GameName;
        public MappingRequestItem[] Mappings;
    }
}
