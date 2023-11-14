using System;
using System.Diagnostics;
using VonRiddarn.BombSwatter;

internal class Program
{
	static void Main(string[] args)
	{

		Board board = new Board(15, 15, 20);
		board.TestStartGame();

		Debug.WriteLine(board.ToString());


		using var game = new VonRiddarn.BombSwatter.Game1();
		game.Run();

		// Todo: Check systems here
	}
}