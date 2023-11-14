using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VonRiddarn.BombSwatter
{
	internal class Entity
	{
		public Color color = Color.White;

		protected Texture2D _texture;
		protected Vector2 _position;

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(_texture, _position, color);
		}

		public void SetTexture(Texture2D texture)
		{
			_texture = texture;
		}

		public void SetPosition(Vector2 position)
		{
			_position = position;
		}
	}
}
