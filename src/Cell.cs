using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VonRiddarn.BombSwatter
{
	
	internal sealed class Cell
	{

		public CellState CellState { get; private set; } = CellState.Default;

		public int AdjacentBombs { get; private set; } = 0;
		public void AddBomb() => AdjacentBombs++;
		public void RemoveBomb() => AdjacentBombs--;


		public bool LeftClick()
		{
			if (CellState == CellState.Default)
				return true;

			return false;
		}

		// Upodate cell state: Disarm -> Flag - > Default -> Repeat
		public void RightClick()
		{
			switch (CellState)
			{
				case CellState.Default:
					CellState = CellState.Disarmed;
					// BombsLeft -= 1;
					break;

				case CellState.Disarmed:
					CellState = CellState.Flagged;
					// BombsLeft += 1;
					break;

				case CellState.Flagged:
					CellState = CellState.Default;
					break;

				// Return if the tile is anything else.
				default:
					return;
			}
		}

	}
}
