using System;

public class Heuristics {
	private static Heuristics instance = null;
	private Heuristics() {}
	private int depthWeights = 25;

	public static Heuristics getInstance() {
		if (instance == null) {
			instance = new Heuristics();
		}

		return instance;
	}


	public int getScore(int[] board, int depth) {
		int score = 0;
		score += winnerOrLoser(board, depth);
		score += killerMove(board, depth);
		score += threeInARow(board, depth);

		return score;
	}


	private int winnerOrLoser(int[] board, int depth) {
		int counter = 0;
		int weights = 200000;

		for (int i = 0; i < board.Length; i++) {
			if ((i & 0x88) == 0) {
				// Check horizontal
				if (i % 16 <= 3 &&
					board[i] != 0 &&
					board[i] == board[i + 1] &&
					board[i + 1] == board[i + 2] &&
					board[i + 2] == board[i + 3] &&
					board[i + 3] == board[i + 4]) {
						counter += board[i];
					}

				// Check vertical
				if (i / 16 <= 3 &&
					board[i] != 0 &&
					board[i] == board[i + 16] &&
					board[i + 16] == board[i + 32] &&
					board[i + 32] == board[i + 48] &&
					board[i + 48] == board[i + 64]) {
						counter += board[i];
					}

				// Check NW -> SE diagonal
				if (i / 16 <= 3 &&
					i % 16 <= 3 &&
					board[i] != 0 &&
					board[i] == board[i + 17] &&
					board[i + 17] == board[i + 34] &&
					board[i + 34] == board[i + 51] &&
					board[i + 51] == board[i + 68]) {
						counter += board[i];
					}

				// Check NE -> SW diagonal
				if (i / 16 <= 3 &&
					i % 16 >= 4 &&
					board[i] != 0 &&
					board[i] == board[i + 15] &&
					board[i + 15] == board[i + 30] &&
					board[i + 30] == board[i + 45] &&
					board[i + 45] == board[i + 60]) {
						counter += board[i];
					}
			}
		}

		return counter * weights - (depth * depthWeights);
	}

	
	private int killerMove(int[] board, int depth) {
		int counter = 0;
		int weights = 300;

		for (int i = 0; i < board.Length; i++) {
			if ((i & 0x88) == 0) {
				// Check horizontal
				if (i % 16 <= 2 &&
					board[i] == 0 &&
					board[i + 1] != 0 &&
					board[i + 1] == board[i + 2] &&
					board[i + 2] == board[i + 3] &&
					board[i + 3] == board[i + 4] &&
					board[i + 5] == 0) {
					counter += board[i + 1];
				}

				// Check vertical
				if (i / 16 <= 2 &&
					board[i] == 0 &&
					board[i + 16] != 0 &&
					board[i + 16] == board[i + 32] &&
					board[i + 32] == board[i + 48] &&
					board[i + 48] == board[i + 64] &&
					board[i + 80] == 0) {
					counter += board[i + 16];
				}

				// Check NW -> SE diagonal
				if (i / 16 <= 2 &&
					i % 16 <= 2 &&
					board[i] == 0 &&
					board[i + 17] != 0 &&
					board[i + 17] == board[i + 34] &&
					board[i + 34] == board[i + 51] &&
					board[i + 51] == board[i + 68] &&
					board[i + 85] == 0) {
						counter += board[i + 17];
					}

				// Check NE -> SW diagonal
				if (i / 16 <= 2 &&
					i % 16 >= 5 &&
					board[i] == 0 &&
					board[i + 15] != 0 &&
					board[i + 15] == board[i + 30] &&
					board[i + 30] == board[i + 45] &&
					board[i + 45] == board[i + 60] &&
					board[i + 75] == 0) {
						counter += board[i + 15];
					}
			}
		}

		return counter * weights - (depth * depthWeights);
	}


	private int threeInARow(int[] board, int depth) {
		int counter = 0;
		int weights = 100;

		for (int i = 0; i < board.Length; i++) {
			if ((i & 0x88) == 0) {
				// Check horizontal
				if (i % 16 <= 3 &&
					board[i] == 0 &&
					board[i + 1] != 0 &&
					board[i + 1] == board[i + 2] &&
					board[i + 2] == board[i + 3] &&
					board[i + 4] == 0) {
					counter += board[i + 1];
				}

				// Check vertical
				if (i / 16 <= 3 &&
					board[i] == 0 &&
					board[i + 16] != 0 &&
					board[i + 16] == board[i + 32] &&
					board[i + 32] == board[i + 48] &&
					board[i + 64] == 0) {
					counter += board[i + 16];
				}

				// Check NW -> SE diagonal
				if (i / 16 <= 3 &&
					i % 16 <= 3 &&
					board[i] == 0 &&
					board[i + 17] != 0 &&
					board[i + 17] == board[i + 34] &&
					board[i + 34] == board[i + 51] &&
					board[i + 68] == 0) {
						counter += board[i + 17];
					}

				// Check NE -> SW diagonal
				if (i / 16 <= 2 &&
					i % 16 >= 5 &&
					board[i] == 0 &&
					board[i + 15] != 0 &&
					board[i + 15] == board[i + 30] &&
					board[i + 30] == board[i + 45] &&
					board[i + 60] == 0) {
						counter += board[i + 15];
					}
			}
		}

		return counter * weights - (depth * depthWeights);
	}
}