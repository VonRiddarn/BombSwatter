using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
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
		bool _active = true;

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
					Color = Color.Black;
					break;
				case 0:
					Color = Color.Green;
					foreach (Cell cell in AdjacentCells)
					{
						if (cell.AdjacentBombs != -1 && cell.CellState == CellState.Default && cell != this)
							cell.Activate();
					}
					break;
				case 1:
					Color = Color.LightCyan;
					break;
				case 2:
					Color = Color.LightBlue;
					break;
				case 3:
					Color = Color.Blue;
					break;
				case 4:
					Color = Color.Yellow;
					break;
				case 5:
					Color = Color.Orange;
					break;
				case 6:
					Color = Color.Red;
					break;
				case 7:
					Color = Color.DarkRed;
					break;
				case 8:
					Color = Color.DarkRed;
					break;
				default:
					break;
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
			if (CellState != CellState.Default)
				return;

			Color = Color.Purple;
			CellState = CellState.Flagged;
		}

		public override void OnHoverEnter()
		{
			if (CellState != CellState.Default)
				return;

			Color = Color.Yellow;
		}

		public override void OnHoverExit()
		{
			if (CellState != CellState.Default)
				return;

			Color = Color.White;
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
