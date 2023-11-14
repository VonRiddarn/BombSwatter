using System;
using System.Collections;

namespace VonRiddarn.BombSwatter
{
	internal sealed class Cell
	{

		static bool _firstClick = true;

		CellState _cellState = CellState.Default;
		Cell[] _adjacentCells = null;

		(int row, int col) _position = (0, 0);
		int _adjacentBombs = 0;
		bool _isBomb = false;

		public void NextCellState()
		{ 
			// Player has right clicked the cell.
			// Toggle the next decorative cell state. Default -> Disarm -> Flag -> Repeat
		}

		public void Activate()
		{ 
			// If this cell is not in the default state, do not activate.
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
