using System;
using System.Collections.Generic;

namespace VonRiddarn.BombSwatter
{
	internal sealed class Board
	{
		// Private
		Random _random = new Random();
		
		Cell[,] _cellMap;
		List<Cell> _bombCells = new List<Cell>();
		
		int _bombAmount = 0;
		int _disarmedAmount = 0;

		public void TestStartGame()
		{
			_cellMap[0, 0].Activate();
		}

		public Board(int width, int height, int bombs)
		{
			_bombAmount = bombs;
			_cellMap = new Cell[width, height];
			ConstructCellsInCellMap();
		}

		void ConstructCellsInCellMap()
		{
			for (int i = 0; i < _cellMap.GetUpperBound(0); i++)
			{
				for (int j = 0; j < _cellMap.GetUpperBound(1); j++)
				{
					_cellMap[i, j] = new Cell(this, (i,j));
				}
			}
		}

		// This will place all the bombs on the board and give all cells around those bombs numbers.
		// Called from the first pressed cell.
		public void GenerateBombs(Cell firstCell)
		{
			// Generate a list of all avalible bomb slots.
			List<Cell> availibleSlots = new List<Cell>();

			foreach (Cell cell in _cellMap)
			{
				availibleSlots.Add(cell);
			}

			// Remove the first cell from the list.
			availibleSlots.Remove(firstCell);

			//// Failsafe to never have too many bombs.
			//if (_bombAmount > availibleSlots.Count)
			//	_bombAmount = availibleSlots.Count - 1;

			for (int i = 0; i < _bombAmount; i++)
			{
				// Get a random number within the range of availible cells.
				// Set that cell to be a bomb
				// Remove that cell from the list of availible cells

				int _ = _random.Next(0, availibleSlots.Count-1);
				_bombCells.Add(availibleSlots[_].MakeBomb());
				availibleSlots.RemoveAt(_);
			}

			// List<Cell> availible = all items in _cellMap
			// availible.Remove(firstCell)
			// Each bomb added availible.Remove(addedBomb)

			// Generate bombs across the board.
			// Bombs can not be placed in cells occupied by other bombs.
			// Bombs can not be placed on the firstCell cell.
		}

		void UpdateBombs()
		{ 
			// Go through all cells that are bombs and add +1 to their adjacent cells.
		}

		Cell[] GetAdjacentCells(Cell cell)
		{
			// Get the adjacent cells from _cellMap[cell.position.row,cell.position.col]
			return null;
		}

		public override string ToString()
		{
			string s = String.Empty;

			for (int i = 0; i < _cellMap.GetUpperBound(0); i++)
			{
				for (int j = 0; j < _cellMap.GetUpperBound(1); j++)
				{
					s += _cellMap[i, j].ToString();
				}
				s += "\n";
			}

			return s;
		}
	}
}
