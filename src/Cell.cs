using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace VonRiddarn.BombSwatter
{
	internal sealed class Cell : Entity
	{

		// Static
		static bool _firstClick = true;

		// Public
		public CellState CellState { get; private set; } = CellState.Default;
		public Cell[] AdjacentCells { get; set; } = null;
		public (int row, int col) CellPosition { get; private set; } = (0, 0);

		// Private
		List<Texture2D> _tileDecors = new List<Texture2D>();

		int _adjacentBombs = 0;
		public int AdjacentBombs
		{
			get { return _isBomb ? -1 : _adjacentBombs; }
		}
		bool _isBomb = false;


		Board _board;

		public Cell(Board board, (int row, int col) cellPosition)
		{
			CellPosition = cellPosition;
			_board = board;
			_firstClick = true;
		}

		public Cell MakeBomb()
		{
			_isBomb = true;
			return this;
		}

		public void AddBomb()
		{
			_adjacentBombs++;
		}

		public void NextCellState()
		{ 
			// Player has right clicked the cell.
			// Toggle the next decorative cell state. Default -> Disarm -> Flag -> Repeat
		}

		public void Activate()
		{
			if (CellState != CellState.Default)
				return;

			CellState = CellState.Activated;

			if (_firstClick)
			{
				_board.GenerateBombs(this);
				_firstClick = false;
			}

			switch (AdjacentBombs)
			{
				case -1:
					_board.LoseGame();
					return;
				case 0:
					foreach (Cell cell in AdjacentCells)
					{
						if (cell.AdjacentBombs != -1 && cell.CellState == CellState.Default && cell != this)
							cell.Activate();
					}
					break;
			}

			ApplyActivateGraphics();

			// If this cell has 0 adjacent bombs, activate all cells around it that also has 0 adjacent bombs.
		}

		public void ForceActivate()
		{
			ApplyForceActivateGraphics();
			CellState = CellState.Activated;
			// Called when the player has lost the game by pressing a bomb.
			// Set the texture to Active and place the decor.
			// Then add whatever state decor the player had applied on top of that decor.
			// [BoxActive.png]
			// [DecorBomb.png]
			// [StateDecorFlag.png]
		}

		// ----- LOGIC & UPDATE -----



		public override void OnLeftMouseDown()
		{
			if (CellState != CellState.Default)
				return;

			Activate();
			Debug.Write(ToString());

		}

		public override void OnRightMouseDown()
		{
			if (CellState == CellState.Activated)
				return;

			ApplyStateUpdateGraphics();
		}

		public override void OnHoverEnter()
		{
			_tileDecors.Add(Data.GetTexture(TextureKeys.TileDecor_Marker));
		}

		public override void OnHoverExit()
		{
			_tileDecors.Remove(Data.GetTexture(TextureKeys.TileDecor_Marker));
		}

		// ----- GRAPHICS & RENDERING -----

		public void ApplyStateUpdateGraphics()
		{
			switch (CellState)
			{
				case CellState.Default:
					CellState = CellState.Disarmed;
					_board.AddFlag();
					_tileDecors.Add(Data.GetTexture(TextureKeys.TileDecor_Flag));
					break;

				case CellState.Disarmed:
					CellState = CellState.Flagged;
					_board.RemoveFlag();
					_tileDecors.Remove(Data.GetTexture(TextureKeys.TileDecor_Flag));
					_tileDecors.Add(Data.GetTexture(TextureKeys.TileDecor_Questionmark));
					break;

				case CellState.Flagged:
					CellState = CellState.Default;
					_tileDecors.Remove(Data.GetTexture(TextureKeys.TileDecor_Questionmark));
					break;

			}
		}

		public void ApplyActivateGraphics()
		{
			_texture = Data.GetTexture(TextureKeys.Tile_Activated);

			// Add tile decor layer 1
			switch (AdjacentBombs)
			{
				case -1:
					_tileDecors.Add(Data.GetTexture(TextureKeys.TileDecor_Bomb));
					break;
				case 1:
					_tileDecors.Add(Data.GetTexture(TextureKeys.TileDecor_1));
					break;
				case 2:
					_tileDecors.Add(Data.GetTexture(TextureKeys.TileDecor_2));
					break;
				case 3:
					_tileDecors.Add(Data.GetTexture(TextureKeys.TileDecor_3));
					break;
				case 4:
					_tileDecors.Add(Data.GetTexture(TextureKeys.TileDecor_4));
					break;
				case 5:
					_tileDecors.Add(Data.GetTexture(TextureKeys.TileDecor_5));
					break;
				case 6:
					_tileDecors.Add(Data.GetTexture(TextureKeys.TileDecor_6));
					break;
				case 7:
					_tileDecors.Add(Data.GetTexture(TextureKeys.TileDecor_7));
					break;
				case 8:
					_tileDecors.Add(Data.GetTexture(TextureKeys.TileDecor_8));
					break;
				default:
					break;
			}
		}

		public void ApplyForceActivateGraphics()
		{
			_texture = Data.GetTexture(TextureKeys.Tile_Activated);

			// Add tile decor layer 1
			switch (AdjacentBombs)
			{
				case -1:
					_tileDecors.Add(Data.GetTexture(TextureKeys.TileDecor_Bomb));
					break;
				case 1:
					_tileDecors.Add(Data.GetTexture(TextureKeys.TileDecor_1));
					break;
				case 2:
					_tileDecors.Add(Data.GetTexture(TextureKeys.TileDecor_2));
					break;
				case 3:
					_tileDecors.Add(Data.GetTexture(TextureKeys.TileDecor_3));
					break;
				case 4:
					_tileDecors.Add(Data.GetTexture(TextureKeys.TileDecor_4));
					break;
				case 5:
					_tileDecors.Add(Data.GetTexture(TextureKeys.TileDecor_5));
					break;
				case 6:
					_tileDecors.Add(Data.GetTexture(TextureKeys.TileDecor_6));
					break;
				case 7:
					_tileDecors.Add(Data.GetTexture(TextureKeys.TileDecor_7));
					break;
				case 8:
					_tileDecors.Add(Data.GetTexture(TextureKeys.TileDecor_8));
					break;
				default:
					break;
			}

			switch (CellState)
			{
				case CellState.Disarmed:
					_tileDecors.Add(Data.GetTexture(TextureKeys.TileDecor_X));
					_tileDecors.Remove(Data.GetTexture(TextureKeys.TileDecor_Flag));
					break;
				case CellState.Flagged:
					_tileDecors.Add(Data.GetTexture(TextureKeys.TileDecor_Box));
					_tileDecors.Remove(Data.GetTexture(TextureKeys.TileDecor_Questionmark));
					break;
				default:
					break;
				}

		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(_texture, _position, Color);
			foreach (Texture2D t in _tileDecors)
			{
				spriteBatch.Draw(t, _position, Color.White);
			}
		}

		public void UpdateCellPosition()
		{
			_position.X = CellPosition.col * _texture.Height;
			_position.Y = CellPosition.row * _texture.Width;
		}

		// Return the cell as a number or an X
		// [X],[0],[1],[2],[3],[4],[5],[6],[7],[8]
		public override string ToString()
		{
			if (_isBomb)
				return "[X]";

			return "[" + _adjacentBombs.ToString() + "]";
		}

	}
}
