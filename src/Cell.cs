using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;

namespace VonRiddarn.BombSwatter
{
	internal sealed partial class Cell : Entity
	{

		// Static
		static bool _firstClick = true;

		// Public
		public Cell[] AdjacentCells { get; set; } = null;
		public (int row, int col) CellPosition { get; private set; } = (0, 0);

		// Private
		int _adjacentBombs = 0;
		bool _isBomb = false;

		CellState _cellState = CellState.Default;
		Board _board;

		public Cell(Board board, (int row, int col) cellPosition)
		{
			CellPosition = cellPosition;
			_board = board;
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
			if (_cellState != CellState.Default)
				return;

			if (_firstClick)
			{
				_board.GenerateBombs(this);
				_firstClick = false;
			}

			// If this cell has 0 adjacent bombs, activate all cells around it that also has 0 adjacent bombs.
		}

		public void ForceActivate()
		{
			// Called when the player has lost the game by pressing a bomb.
			// Set the texture to Active and place the decor.
			// Then add whatever state decor the player had applied on top of that decor.
			// [BoxActive.png]
			// [DecorBomb.png]
			// [StateDecorFlag.png]
		}


		// ----- GRAPHICS & RENDERING -----

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
