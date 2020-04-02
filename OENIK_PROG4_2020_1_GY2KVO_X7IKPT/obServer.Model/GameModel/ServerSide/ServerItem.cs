using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace obServer.Model.GameModel.ServerSide
{
    public struct ServerItem
    {
		private Rect bounds;

		public Rect Bounds
		{
			get { return bounds; }
			set { bounds = value; }
		}

		private string type;

		public string Type
		{
			get { return type; }
			set { type = value; }
		}

		private Guid id;

		public Guid Id
		{
			get { return id; }
			set { id = value; }
		}
	}
}
