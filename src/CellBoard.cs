using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VonRiddarn.BombSwatter
{
	internal sealed class CellBoard
	{
		// IMPORTANT NOTE
		// 2D arrays move top -> bottom.
		// This means that we need to start all coordinates with Y instead of X.
		// 0,0 = top left.
		// [0,0] [0,1] [0,1]
		// [1,0] [1,1] [1,2]
		// [2,0] [2,1] [2,2]

		Cell[,] _cellMap = null;
		(int y, int x)[] _bombPositions = null;

		int _bombs = 0;


		// Loop through all the bombs and add a number to their neighbour.
		// This is more efficent than looping through all cells and checking their neighbours.
		void UpdateCellNumbers()
		{
			for (int i = 0; i < _bombPositions.Length; i++)
			{
				foreach (Cell cell in GetAdjacentCells(_bombPositions[i]))
				{
					cell.AddBomb();
				}
			}
		}

		// Get the 8 adjacent cells of the inputted position.
		// Kind of lazy as we just use a try-catch to ignore any out of bounds errors.
		Cell[] GetAdjacentCells((int y, int x) position)
		{
			List<Cell> tempCells = new List<Cell>();

			for (int i = -1; i < 2; i++)
			{
				for (int j = -1; j < 2; j++)
				{
					// Get the direction to check.
					// Top left = position -1,-1
					// Bottom right = position +1, +1
					int directionY = position.y - i;
					int directionX = position.x - j;

					//If this is the parameter position, do nothing.
					if (position.y == directionY && position.x == directionX)
						continue;
					try
					{
						tempCells.Add(_cellMap[directionY, directionX]);
					}
					catch { } // Array out of bounds, do nothing.
				}
			}

			return tempCells.ToArray();
		}
	}
}
