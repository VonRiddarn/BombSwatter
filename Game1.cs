using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace VonRiddarn.BombSwatter
{
	public class Game1 : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;

		Board board = new Board(15, 25, 25);
		Texture2D cellTexture;

		bool restartFlag = false;

		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			// TODO: Add your initialization logic here
			_graphics.PreferredBackBufferWidth = board.Width * 32;
			_graphics.PreferredBackBufferHeight = board.Height * 32;
			_graphics.ApplyChanges();

			base.Initialize();
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			// TODO: use this.Content to load your game content here
			Data.LoadTextures(Content);
			board.InitializeAllCellTextures();

		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			if (Keyboard.GetState().IsKeyDown(Keys.R) && !restartFlag)
			{
				board = new Board(15, 25, 25);
				board.InitializeAllCellTextures();
				restartFlag = true;
			}
			if(Keyboard.GetState().IsKeyUp(Keys.R))
				restartFlag = false;

			// TODO: Add your update logic here
			board.UpdateCells();

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			// TODO: Add your drawing code here
			_spriteBatch.Begin();

			board.DrawCells(_spriteBatch);

			_spriteBatch.End();


			base.Draw(gameTime);
		}
	}
}