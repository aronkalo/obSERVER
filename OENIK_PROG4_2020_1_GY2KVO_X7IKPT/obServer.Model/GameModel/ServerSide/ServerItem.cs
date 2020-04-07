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

		private bool impact;

		public bool Impact
		{
			get { return impact; }
			set { impact = value; }
		}

		private Vector direction;

		public Vector Direction
		{
			get { return direction; }
			set { direction = value; }
		}

		private Vector startPoint;

		public Vector StartPoint
		{
			get { return startPoint; }
			set { startPoint = value; }
		}

		private string name;

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		private double speed;

		public double Speed
		{
			get { return speed; }
			set { speed = value; }
		}

		private double weight;

		public double Weight
		{
			get { return weight; }
			set { weight = value; }
		}

		private bool owned;

		public bool Owned
		{
			get { return owned; }
			set { owned = value; }
		}


		public override bool Equals(object obj)
		{
			if ((obj as ServerItem?).Value.Id == this.Id)
			{
				return true;
			}
			return false;
		}
	}
}
