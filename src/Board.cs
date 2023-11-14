namespace VonRiddarn.BombSwatter
{
	internal sealed class Board
	{
		Cell[,] _cellMap;



		// This will place all the bombs on the board and give all cells around those bombs numbers.
		void GenerateBombs(Cell fisrtCell)
		{ 
			// List<Cell> availible = all items in _cellMap
			// availible.Remove(firstCell)
			// Each bomb added availible.Remove(addedBomb)

			// Generate bombs across the board.
			// Bombs can not be placed in cells occupied by other bombs.
			// Bombs can not be placed on the firstCell cell.
		}

		void UpdateBoard()
		{ 
			// Go through all cells that are bombs and add +1 to their adjacent cells.
		}

		Cell[] GetAdjacentCells(Cell cell)
		{
			// Get the adjacent cells from _cellMap[row,col]
			return null;
		}
	}
}
