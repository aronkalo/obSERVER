using obServer.Network.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace obServer.GameLogic
{
    public abstract class BaseLogic
    {
        virtual protected void HandleRequests(Request request) { }

        virtual protected void HandleMove(Request request) { }

        virtual protected void HandleConnect(Request request) { }

        virtual protected void HandleDisconnect(Request request) { }

        virtual protected void HandleShoot(Request request) { }

        virtual protected void HandleDie(Request request) { }

        virtual protected void HandleSendObject(Request request) { }

        virtual protected void HandlePickup(Request request) { }

        virtual protected void HandleRemove(Request request) { }

        virtual protected void HandleReady(Request request) { }

        virtual protected void HandleHit(Request request) { }

        virtual protected void HandleSendMessage(Request request) { }

        public double Milis
        {
            get
            {
                return DateTime.Now.Ticks;
            }
        }

    }
}
