﻿using obServer.Model.Interfaces;
using System;
using System.Windows.Media;

namespace obServer.Model.GameModel.Item
{
	public abstract class BaseItem : IBaseItem
	{
		public BaseItem(Geometry geometry, Guid id, double[] position, double rotation, bool impact)
		{
			primitve = geometry;
			Id = id;
			Position = new double[2];
			SetPosition(position[0], position[1]);
			Rotation = rotation;
			Impact = impact;
			changed = true;
		}

		private Guid id;

		public Guid Id
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
					primitve.Transform = new TranslateTransform(Position[0], Position[1]);
					cache = primitve.GetFlattenedPathGeometry();
					cache.Transform = new RotateTransform(Rotation, Position[0], Position[1]);
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
			changed = true;
		}

		virtual protected void ChangePosition(double x, double y, double angle = 0)
		{
			Position[0] = Position[0] + x;
			Position[1] = Position[1] + y;
			Rotation = angle;
			changed = true;
		}
	}
}
