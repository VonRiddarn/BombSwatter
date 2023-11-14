using System;
using System.Collections;

namespace VonRiddarn.BombSwatter
{
	internal sealed class Cell
	{

		// Static
		static bool _firstClick = true;

		// Public
		public (int row, int col) Position { get; private set; } = (0, 0);

		// Private
		int _adjacentBombs = 0;
		bool _isBomb = false;

		CellState _cellState = CellState.Default;
		Cell[] _adjacentCells = null;
		Board _board;

		public Cell(Board board, (int row, int col) position)
		{
			Position = position;
			_board = board;
		}

		public override string ToString()
		{
			return _isBomb ? "1" : "0";
		}

		public Cell MakeBomb()
		{
			_isBomb = true;
			return this;
		}

		void AddBomb()
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

	}
}
