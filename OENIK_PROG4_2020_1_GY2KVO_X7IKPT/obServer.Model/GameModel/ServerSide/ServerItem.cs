using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace obServer.Model.GameModel.ServerSide
{
    public struct ServerItem
    {
		private double[] position;

		public double[] Position
		{
			get { return position; }
			set { position = value; }
		}

		private double[] dimensions;

		public double[] Dimensions
		{
			get { return dimensions; }
			set { dimensions = value; }
		}

		private string type;

		public string Type
		{
			get { return type; }
			set { type = value; }
		}

		private double rotation;

		public double Rotation
		{
			get { return rotation; }
			set { rotation = value; }
		}

		private Guid id;

		public Guid Id
		{
			get { return id; }
			set { id = value; }
		}
	}
}
