using System;
using System.Net;

namespace obServer.Network.Structs
{
    public class GameClientInfo
    {
		private IPEndPoint endpoint;
		public IPEndPoint EndPoint
		{
			get { return endpoint; }
			set { endpoint = value; }
		}

		private Guid id;
		public Guid Id
		{
			get { return id; }
			set { id = value; }
		}

		private bool ready;
		public bool Ready
		{
			get { return ready; }
			set { ready = value; }
		}

		public bool SetReady(string ipAddress)
		{
			if (ipAddress == endpoint.Address.ToString())
			{
				Ready = true;
				return true;
			}
			return false;
		}
	}
}