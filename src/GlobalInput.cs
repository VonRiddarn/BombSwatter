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

		// Public

		// This is ugly.
		// TODO: Change into "OnMouseDown(int buttonIndex)" later.
		// TODO: Remove all these events and find a way to do it where I don't subscribe all the Entities to the input system.


		// OnLeftMouseDown(position)
		// Check if HoverPosition(position)
		// Flag press for 1 frame
		// Apply Hold whilst being inside the HoverPosition().
		public static event Action OnLeftMouseDown;
		public static event Action OnLeftMouseHold;
		public static event Action OnLeftMouseUp;

		public static event Action OnRightMouseDown;
		public static event Action OnRightMouseHold;
		public static event Action OnRightMouseUp;

		// Private
		static MouseState _mouseState;

		// Flags
		static bool _mouse0Hold = false;
		static bool _mouse1Hold = false;

		public static void Update()
		{
			_mouseState = Mouse.GetState();
			UpdateMouseEvents();
		}

		static void UpdateMouseEvents()
		{
			switch (_mouseState.LeftButton)
			{
				case ButtonState.Pressed:
					if (!_mouse0Hold)
						OnLeftMouseDown?.Invoke();

					OnLeftMouseHold?.Invoke();
					break;

				case ButtonState.Released:
					if (_mouse0Hold)
						OnLeftMouseUp?.Invoke();
					break;

				default:
					break;
			}

			switch (_mouseState.RightButton)
			{
				case ButtonState.Pressed:
					if (!_mouse1Hold)
						OnRightMouseDown?.Invoke();

					OnRightMouseHold?.Invoke();
					break;

				case ButtonState.Released:
					if (_mouse1Hold)
						OnRightMouseUp?.Invoke();
					break;

				default:
					break;
			}
		}


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
