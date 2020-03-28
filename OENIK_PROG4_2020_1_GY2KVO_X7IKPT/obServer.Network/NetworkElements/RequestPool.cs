using System;
using obServer.Network.Structs;

namespace obServer.Network.NetworkElements
{
    internal sealed class RequestPool : BasePool<Request>
    {
        public Request GetRequest()
        {
            return GetPoolElement();
        }
        protected override Request GetPoolElement()
        {
            if (NotNullElement())
            {
                return base.GetPoolElement();
            }
            else
            {
                throw new Exception("RequestPool Contains no element");
            }
        }
    }
}
