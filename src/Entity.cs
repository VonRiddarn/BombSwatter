using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace VonRiddarn.BombSwatter
{
	internal abstract class Entity
	{
		public int Width { get { return _texture.Width; } }
		public int Height { get { return _texture.Height; } }
		public Color Color { get; protected set; } = Color.White;

		protected Texture2D _texture;
		protected Vector2 _position;
		protected bool _allowEvents = true;

		// Flags
		bool _isHovering = false;

		static bool _isHoldingMouse0 = false;
		static bool _isHoldingMouse1 = false;

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(_texture, _position, Color);
		}

		public void SetTexture(Texture2D texture)
		{
			_texture = texture;
		}

		public void SetPosition(Vector2 position)
		{
			_position = position;
		}

		public bool OverLapWithMouse(MouseState mouse)
		{
			Point mp = new Point(mouse.X, mouse.Y);
			return (mp.X >= _position.X && mp.Y >= _position.Y && mp.X < _position.X + Width && mp.Y < _position.Y + Height);
		}


		// This entire method is a dumpster fire.
		// This was just to get things going and I can't be bothered to fix it for such a trival project.
		// It will look better in the game engine project.
		public virtual void UpdateEvents()
		{
			if (!_allowEvents)
				return;

			MouseState ms = Mouse.GetState();

			if (!_isHoldingMouse0 && !_isHoldingMouse1)
			{
				if (OverLapWithMouse(ms))
				{
					if (!_isHovering)
					{
						OnHoverEnter();
						_isHovering = true;
					}
					OnHoverStay();
				}
				else
				{
					if (!_isHovering)
						return;

					OnHoverExit();
					_isHovering = false;
				}
			}

			// It's bad that we check for mouse input like this.
			// I would much rather centralize a method in some input class.
			// I'd also like to be able to "select items" and answer to button release in an appropriate manner.
			// For my next project I will write a simple game engine - these problems will be adressed then.
			// For this project, this is enough.

			if (ms.LeftButton == ButtonState.Pressed)
			{
				if (!_isHoldingMouse0)
				{
					_isHoldingMouse0 = true;

					if (OverLapWithMouse(ms))
						OnLeftMouseDown();
				}
			}
			else
				_isHoldingMouse0 = false;

			if (ms.RightButton == ButtonState.Pressed)
			{
					if (!_isHoldingMouse1)
					{
						_isHoldingMouse1 = true;

						if (OverLapWithMouse(ms))
							OnRightMouseDown();
					}
			}
			else
				_isHoldingMouse1 = false;

		}


		// Event calls
		// Might translate these to actual events later?

		public virtual void OnLeftMouseDown()
		{ 
		
		}

		public virtual void OnRightMouseDown()
		{

		}

		public virtual void OnHoverEnter()
		{ 
			
		}

		public virtual void OnHoverStay()
		{ 
			
		}

		public virtual void OnHoverExit()
		{

		}
	}
}
