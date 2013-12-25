using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkController.Client.Logic
{
    public class ReceiverSingleton
    {
        private static Receiver _instance;
        public static Receiver Instance
        {
            get { return _instance ?? (_instance = new Receiver()); }
        }
    }
}
