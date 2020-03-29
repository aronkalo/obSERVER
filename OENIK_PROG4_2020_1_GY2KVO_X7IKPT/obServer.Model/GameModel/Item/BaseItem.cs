using System.Windows.Media;

namespace obServer.Model.GameModel.Item
{
	public abstract class BaseItem
	{
		public BaseItem(Geometry geometry, string id, double[] position, double rotation, bool impact)
		{
			Id = id;
			Position = new double[2];
			SetPosition(position[0], position[1]);
			Rotation = rotation;
			primitve = geometry;
			Impact = impact;
			changed = true;
		}

		private string id;

		public string Id
		{
			get { return id; }
			set { id = value; }
		}


		private double[] position;

		public double[] Position
		{
			get { return position; }
			set { position = value; }
		}

		private double rotation;

		public double Rotation
		{
			get { return rotation; }
			set 
			{ 
				rotation = value;
				changed = true;
			}
		}

		private bool impact;

		public bool Impact
		{
			get { return impact; }
			set 
			{ 
				impact = value;
				changed = true;
			}
		}

		private Geometry primitve;

		private Geometry cache;

		private bool changed;

		public Geometry RealPrimitive
		{
			get 
			{
				if (changed)
				{
					TransformGroup tg = new TransformGroup();
					tg.Children.Add(new TranslateTransform(Position[0], Position[1]));
					tg.Children.Add(new RotateTransform(Rotation, Position[0], Position[1]));
					primitve.Transform = tg;
					cache = primitve.GetFlattenedPathGeometry();
					changed = false;
				}
				return cache;
			}
		}

		public bool CollidesWith(Geometry geometry)
		{
			if (impact)
			{
				double area = Geometry.Combine(RealPrimitive, geometry, GeometryCombineMode.Intersect, null).GetArea();
				if (area > 0)
				{
					return true;
				}
				return false;
			}
			return false;
		}

		virtual protected void SetPosition(double x, double y)
		{
			Position[0] = x;
			Position[1] = y;
		}

		virtual protected void ChangePosition(double x, double y, double angle = 0)
		{
			Position[0] = Position[0] + x;
			Position[1] = Position[1] + y;
			Rotation = angle;
		}
	}
}
