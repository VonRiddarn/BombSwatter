using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace VonRiddarn.BombSwatter
{
	internal static class Data
	{
		public static Dictionary<TextureKeys, Texture2D> Textures = new Dictionary<TextureKeys, Texture2D>();

		public static void LoadTextures(ContentManager content)
		{
			Textures.Add(TextureKeys.Tile_Default, content.Load<Texture2D>("Tile_Default"));
			Textures.Add(TextureKeys.Tile_Activated, content.Load<Texture2D>("Tile_Activated"));
			Textures.Add(TextureKeys.TileDecor_Flag, content.Load<Texture2D>("TileDecor_Flag"));
			Textures.Add(TextureKeys.TileDecor_Questionmark, content.Load<Texture2D>("TileDecor_Questionmark"));
			Textures.Add(TextureKeys.TileDecor_Bomb, content.Load<Texture2D>("TileDecor_Bomb"));
			Textures.Add(TextureKeys.TileDecor_X, content.Load<Texture2D>("TileDecor_X"));
			Textures.Add(TextureKeys.TileDecor_Box, content.Load<Texture2D>("TileDecor_Box"));
			Textures.Add(TextureKeys.TileDecor_1, content.Load<Texture2D>("TileDecor_1"));
			Textures.Add(TextureKeys.TileDecor_2, content.Load<Texture2D>("TileDecor_2"));
			Textures.Add(TextureKeys.TileDecor_3, content.Load<Texture2D>("TileDecor_3"));
			Textures.Add(TextureKeys.TileDecor_4, content.Load<Texture2D>("TileDecor_4"));
			Textures.Add(TextureKeys.TileDecor_5, content.Load<Texture2D>("TileDecor_5"));
			Textures.Add(TextureKeys.TileDecor_6, content.Load<Texture2D>("TileDecor_6"));
			Textures.Add(TextureKeys.TileDecor_7, content.Load<Texture2D>("TileDecor_7"));
			Textures.Add(TextureKeys.TileDecor_8, content.Load<Texture2D>("TileDecor_8"));
		}

	}
}
