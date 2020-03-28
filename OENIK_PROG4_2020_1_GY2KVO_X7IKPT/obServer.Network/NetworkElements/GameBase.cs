using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace obServer.Network.NetworkElements
{
    public abstract class GameBase
    {
		public GameBase()
		{
			sw = new Stopwatch();
			sw.Start();
		}
		private Stopwatch sw { get; set;}
		public double ConnectionTime
		{
			get { return sw.ElapsedMilliseconds/1000;}
		}
	}
}
