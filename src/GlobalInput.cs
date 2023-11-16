using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombSwatter.src
{
	public static class GlobalInput
	{
		// Private
		static MouseState _mouseState;


		// This is good, well keep this.
		public static bool HoverPosition(int x, int y, int width, int height)
		{
			Point mp = new Point(_mouseState.X, _mouseState.Y);

			if (mp.X >= x && mp.Y >= y && mp.X < x + width && mp.Y < y + height)
				return true;

			return false;
		}
	}
}
