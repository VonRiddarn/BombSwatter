using System;
using System.Diagnostics;
using VonRiddarn.BombSwatter;

internal class Program
{
	static void Main(string[] args)
	{
		//using var game = new VonRiddarn.BombSwatter.Game1();
		//game.Run();

		// Todo: Check systems here

		Board board = new Board(15, 15, 50);
		board.TestStartGame();

		Debug.WriteLine(board.ToString());
	}
}