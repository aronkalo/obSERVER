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
		public GameBase(string Name)
		{
			this.Name = Name;
			sw = new Stopwatch();
			sw.Start();
		}
		private string name;
		public string Name
		{
			get { return name; }
			set { name = value; }
		}
		private Stopwatch sw { get; set;}
		public double ConnectionTime
		{
			get { return sw.ElapsedMilliseconds/1000;}
		}
	}
}
