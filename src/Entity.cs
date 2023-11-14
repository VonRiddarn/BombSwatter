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

		bool _isHovering = false;
		bool _isHoldingMouse = false;

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

		public virtual void UpdateEvents()
		{
			if (!_allowEvents)
				return;

			MouseState ms = Mouse.GetState();
			Point mp = new Point(ms.X, ms.Y);

			if (mp.X >= _position.X && mp.Y >= _position.Y && mp.X < _position.X + Width && mp.Y < _position.Y + Height)
			{
				if (!_isHovering)
				{
                    OnHoverEnter();
					_isHovering = true;
				}
				else
				{
					OnHoverStay();
				}
			}
			else
			{
				if (!_isHovering)
					return;
				 
				OnHoverExit();
				_isHovering = false;
			}
		}


		// Event calls
		// Might translate these to actual events later?

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
