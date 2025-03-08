using System;
using System.Collections.Generic;
using System.Text;

namespace Hello_Console
{
	abstract class Animal
	{
		public int X
		{
			get
			{
				return X;
			}

			set
			{
				X = value;
			}
		}

		public int Y
		{
			get
			{
				return Y;
			}

			set
			{
				Y = value;
			}
		}

		public int Dx
		{
			get
			{
				return Dx;
			}

			set
			{
				Dx = value;
			}

		}

		public int Dy
		{
			get
			{
				return Dy;
			}

			set
			{
				Dy = value;
			}
		}


		public abstract void Voice();
		public abstract void Move(int Dx, int Dy);
	}
}