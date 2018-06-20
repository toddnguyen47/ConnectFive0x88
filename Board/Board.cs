using System;

public class Board {
	public static int BOARD_SIZE = 256;
	private static Board instance = null;

	private int[] board = new int[BOARD_SIZE];
	private String[] rowChar = {"a", "b", "c", "d", "e", "f", "g", "h"};
	private String[] colChar = {"1", "2", "3", "4", "5", "6", "7", "8"};
	private System.Collections.ArrayList moveList = new System.Collections.ArrayList();

	private int numPieces;
	private Boolean maxTurn;

	private Board() {}
	public static Board getInstance() {
		if (instance == null) {
			instance = new Board();
		}
		return instance;
	}

	public void initializeBoard(Boolean maxTurn) {
		for (int i = 0; i < BOARD_SIZE; i++) {
			if ((i & 0x88) == 0)
				board[i] = 0;
		}

		numPieces = 0;
		this.maxTurn = maxTurn;
	}


	public void printBoard() {
		Console.WriteLine("\n  1 2 3 4 5 6 7 8");

		for (int i = 0; i < BOARD_SIZE; i++) {
			if ((i & 0x88) == 0) {
				if (i % 8 == 0) {Console.Write(rowChar[i / 16] + " ");}

				if (board[i] == 1) {Console.Write("X ");}
				else if (board[i] == -1) {Console.Write("O ");}
				else Console.Write("- ");

				if (i % 8 == 7) {Console.Write("\n");}
			}
		}

		Console.WriteLine("");
		if (moveList.Count > 1) {
			for (int jj = 0; jj < moveList.Count; jj++) {
				if (jj % 2 == 0) {
					Console.Write(String.Format("[{0}] ", (jj / 2) + 1));
					Console.Write(moveList[jj] + " ");
				}
				else {
					Console.Write(moveList[jj] + "  ");
				}

				if (jj % 8 == 7) Console.WriteLine("");
			}

			if (moveList.Count >= 2) {
				int n = moveList.Count;
				Console.Write("\nPrevious 2 moves: " + moveList[n - 2] + " " + moveList[n - 1]);
			}
			Console.WriteLine("\n");
		}
	}

	// Check if the move is valid
	public bool validMove(int move) {
		if ((move & 0x88) != 0) return false;

		return board[move] == 0;
	}


	public bool checkForValidMove(int move) {
		bool result = this.validMove(move);
		if (!result) {
			Console.WriteLine("Invalid Move!");
		}

		return result;
	}


	public bool placeOnBoard(int move) {
		if (!validMove(move)) {
			Console.WriteLine("Invalid placing move!");
			return false;
		}

		if (maxTurn) board[move] = 1;
		else board[move] = -1;

		numPieces += 1;
		maxTurn = !maxTurn;

		int row = move / 16;
		int col = move % 16;
		String temp = rowChar[row] + colChar[col];
		moveList.Add(temp);

		return true;
	}


	public Boolean resetBoard(int move) {
		if ((move & 0x88) != 0) {
			Console.WriteLine("Invalid reset move!");
		}

		board[move] = 0;
		numPieces -= 1;
		maxTurn = !maxTurn;
		moveList.RemoveAt(moveList.Count - 1);

		return true;
	}


	public bool isTerminalBoard() {
		int zeroCounter = 0;
		for (int i = 0; i < Board.BOARD_SIZE; i++) {
			if ((i & 0x88) == 0) {
				// Check horizontal
				if (i % 16 <= 3 &&
					board[i] != 0 &&
					board[i] == board[i + 1] &&
					board[i + 1] == board[i + 2] &&
					board[i + 2] == board[i + 3] &&
					board[i + 3] == board[i + 4]) {
						return true;
					}

				// Check vertical
				if (i / 16 <= 3 &&
					board[i] != 0 &&
					board[i] == board[i + 16] &&
					board[i + 16] == board[i + 32] &&
					board[i + 32] == board[i + 48] &&
					board[i + 48] == board[i + 64]) {
						return true;
					}

				// Check NW -> SE diagonal
				if (i / 16 <= 3 &&
					i % 16 <= 3 &&
					board[i] != 0 &&
					board[i] == board[i + 17] &&
					board[i + 17] == board[i + 34] &&
					board[i + 34] == board[i + 51] &&
					board[i + 51] == board[i + 68]) {
						return true;
					}

				// Check NE -> SW diagonal
				if (i / 16 <= 3 &&
					i % 16 >= 4 &&
					board[i] != 0 &&
					board[i] == board[i + 15] &&
					board[i + 15] == board[i + 30] &&
					board[i + 30] == board[i + 45] &&
					board[i + 45] == board[i + 60]) {
						return true;
					}

				if (board[i] == 0) zeroCounter += 1;
			}
		}

		return zeroCounter == 0;
	}


	public int[] getBoard() => this.board;
	public bool getMaxTurn() => this.maxTurn;
	public String[] getRowChar() => this.rowChar;
	public String[] getColChar() => this.colChar;
}