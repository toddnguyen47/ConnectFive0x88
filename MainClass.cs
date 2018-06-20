// Namespace declaration
using System;

public class MainClass {
	private int getTimeToThink() {
		Console.Write("Enter time to think (in seconds): ");
		int seconds;
		if (!int.TryParse(Console.ReadLine(), out seconds)) {
			Console.Write("Enter time to think (in seconds): ");
		}

		return seconds;
	}


	private int getMove() {
		Board board = Board.getInstance();
		Console.Write("Enter move. Enter -1 to quit: ");
		String output = Console.ReadLine();
		if (output.Equals("-1")) {this.endProgram();}

		int row = output[0] - 'a';
		int col = output[1] - '1';
		int move = row * 16 + col;

		while (!board.checkForValidMove(move)) {
			Console.Write("Enter move. Enter -1 to quit: ");
			output = Console.ReadLine();
			if (output.Equals("-1")) {this.endProgram();}

			row = output[0] - 'a';
			col = output[1] - '1';
			move = row * 16 + col;
		}

		return move;
	}


	private void endProgram() {
		Console.WriteLine("Thank you for playing!");
		Environment.Exit(0);
	}


	private bool otherPlayerGoesFirst() {
		Console.Write("Does the other player/AI go first? (Y/N): ");
		String input = Console.ReadLine();
		input = input.ToLower();
		while (!input.Equals("n") && !input.Equals("y")) {
			Console.Write("Does the other player/AI go first? (Y/N): ");
			input = Console.ReadLine();
			input = input.ToLower();
		}
		
		bool otherPlayer = input.Equals("y");
		return otherPlayer;
	}


	public static void Main() {
		Board board = Board.getInstance();
		MainClass main = new MainClass();
		int timeToThink = main.getTimeToThink();

		board.initializeBoard(main.otherPlayerGoesFirst());
		int move = 3 * 16 + 3; // d4 is initial move
		MiniMax_IterativeDeepening MiniMaxIDS = new MiniMax_IterativeDeepening(timeToThink);

		while (!board.isTerminalBoard()) {
			// This AI's turn
			if (board.getMaxTurn()) {
				board.printBoard();
				move = main.getMove();
				board.placeOnBoard(move);
			}
			// Other player/other AI's turn
			else {
				// move = main.getMove();
				// board.placeOnBoard(move);
				board.printBoard();
				Console.WriteLine("AI is thinking...");
				move = MiniMaxIDS.execute(move);
				board.placeOnBoard(move);
			}
		}

		board.printBoard();
		// This AI's turn
		if (board.getMaxTurn()) {
			Console.WriteLine("This AI won!");
		}
		else {
			Console.WriteLine("The player/other AI won!");
		}
	}
}