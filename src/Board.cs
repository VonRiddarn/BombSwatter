using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace VonRiddarn.BombSwatter
{
	internal sealed class Board
	{
		// IMPORTANT NOTE
		// 2D arrays move top -> bottom.
		//		 Col 0	Col 1	Col 2
		// Row 0 [0,0]	[0,1]	[0,1]
		// Row 1 [1,0]	[1,1]	[1,2]
		// Row 2 [2,0]	[2,1]	[2,2]
		// When nesting loops, nest row as i and column as j.

		// Public

		public int Width { get; private set; }
		public int Height { get; private set; }

		// Private
		Random _random = new Random();
		
		Cell[,] _cellMap;
		List<Cell> _bombCells = new List<Cell>();
		
		int _bombAmount = 0;
		int _disarmedAmount = 0;

		// When called "presses" the cell at the top left on the board.
		// Used to debug the game.
		public void TestStartGame()
		{
			_cellMap[0, 0].Activate();
		}

		public Board(int width, int height, int bombs)
		{
			Width = width;
			Height = height;

			_bombAmount = bombs;
			_cellMap = new Cell[height, width];
			ConstructCellsInCellMap();
		}

		void ConstructCellsInCellMap()
		{
			for (int i = 0; i < _cellMap.GetLength(0); i++)
			{
				for (int j = 0; j < _cellMap.GetLength(1); j++)
				{
					_cellMap[i, j] = new Cell(this, (i,j));
				}
			}

			foreach (Cell cell in _cellMap)
			{
				cell.AdjacentCells = _GetAdjacentCells(cell);
			}

		}

		// This will place all the bombs on the board and give all cells around those bombs numbers.
		// Called from the first cell the player interacts with.
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

			// Failsafe to never have too many bombs.
			if (_bombAmount > availibleSlots.Count)
				_bombAmount = availibleSlots.Count;

			for (int i = 0; i < _bombAmount; i++)
			{
				// Get a random number within the range of availible cells.
				// Set that cell to be a bomb
				// Remove that cell from the list of availible cells

				int _ = _random.Next(0, availibleSlots.Count-1);
				_bombCells.Add(availibleSlots[_].MakeBomb());
				availibleSlots.RemoveAt(_);
			}

			// Update all the numbers of cells adjacent to bombs.
			foreach (Cell bomb in _bombCells)
			{
				foreach (Cell cell in bomb.AdjacentCells)
				{
					cell.AddBomb();
				}
			}
		}


		Cell[] _GetAdjacentCells(Cell cell)
		{
			List<Cell> tempCells = new List<Cell>();

			for (int i = -1; i < 2; i++) // Row offset
			{
				for (int j = -1; j < 2; j++) // Column offset
				{

					// Get the direction to check.
					// Top left [-1,-1] || Bottom right [+1, +1]
					int directionY = cell.CellPosition.row + i;
					int directionX = cell.CellPosition.col + j;


					// Skip this itteration if we are on the parameter cell position.
					if (cell.CellPosition.row == directionY && cell.CellPosition.col == directionX)
						continue;


					// This is a kind of lazy implementation.
					// We simply let the array fall out of range and catch the error without handling it.
					// No idea how this affects performance, but it couldn't be too bad.
					try
					{
						tempCells.Add(_cellMap[directionY, directionX]);
					}
					catch { } // Array out of bounds, do nothing.
				}
			}

			return tempCells.ToArray();
		}



		// ----- GRAPHICS & RENDERING -----

		public void DrawCells(SpriteBatch spriteBatch)
		{
			foreach (Cell cell in _cellMap)
			{
				cell.Draw(spriteBatch);
			}
		}

		public void SetAllCellTextures(Texture2D texture)
		{
			foreach (Cell cell in _cellMap)
			{
				cell.SetTexture(texture);
			}
		}

		public void SetCellTexture(Cell cell, Texture2D texture)
		{
			cell.SetTexture(texture);
		}


		// Return the board as ASCII art.
		// [X][2][0][0]
		// [3][X][2][0]
		// [2][X][2][0]
		public override string ToString()
		{
			string s = String.Empty;

			for (int i = 0; i < _cellMap.GetLength(0); i++)
			{
				for (int j = 0; j < _cellMap.GetLength(1); j++)
				{
					s += _cellMap[i, j].ToString();
				}
				s += "\n";
			}

			return s;
		}

	}
}
