using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VonRiddarn.BombSwatter
{
	internal class Entity
	{
		public Color color = Color.White;

		protected Texture2D _texture;
		protected Vector2 _position;

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(_texture, _position, color);
		}
	}
}
