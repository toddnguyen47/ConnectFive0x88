using System;
using System.Collections;

public class Successors {
	private static Successors instance = null;
	private Successors() {}

	public static Successors getInstance() {
		if (instance == null) {
			instance = new Successors();
		}

		return instance;
	}


	public ArrayList getSuccessors(int prevMove) {
		Board board = Board.getInstance();
		ArrayList successors = new ArrayList();
		int[] directions = {0, 1, 2, 3};

		for (int i = 1; i < 8; i++) {
			int[] shuffled = this.shuffle(directions);

			foreach (int dir in shuffled) {
				int startBound = 0;
				int endBound = 0;
				int diff = 0;
				switch (dir) {
					// North
					case 0:
						startBound = prevMove - (17 * i);
						endBound = prevMove - (15 * i);
						diff = 1;
						break;
					// East
					case 1:
						startBound = prevMove - (15 * i);
						endBound = prevMove + (17 * i);
						diff = 16; 
						break;
					// South
					case 2:
						startBound = prevMove + (17 * i);
						endBound = prevMove + (15 * i);
						diff = -1;
						break;
					// West
					case 3:
						startBound = prevMove + (15 * i);
						endBound = prevMove - (17 * i);
						diff = -16;
						break;
				}

				if (diff > 0) {
					for (int jj = startBound; jj < endBound; jj += diff) {
						if (board.validMove(jj)) {
							successors.Add(jj);
						}		
					}
				}
				else {
					for (int jj = startBound; jj > endBound; jj += diff) {
						if (board.validMove(jj)) {
							successors.Add(jj);
						}		
					}
				}
			}
		}

		return successors;
	}


	public ArrayList getSuccessors2(int prevMove, int bestMove) {
		Board board = Board.getInstance();
		ArrayList successors = new ArrayList();
		successors.Add(bestMove);
		int[] directions = {0, 1, 2, 3};

		for (int i = 1; i < 8; i++) {
			int[] shuffled = this.shuffle(directions);

			foreach (int dir in shuffled) {
				int startBound = 0;
				int endBound = 0;
				int diff = 0;
				switch (dir) {
					// North
					case 0:
						startBound = prevMove - (17 * i);
						endBound = prevMove - (15 * i);
						diff = 1;
						break;
					// East
					case 1:
						startBound = prevMove - (15 * i);
						endBound = prevMove + (17 * i);
						diff = 16; 
						break;
					// South
					case 2:
						startBound = prevMove + (17 * i);
						endBound = prevMove + (15 * i);
						diff = -1;
						break;
					// West
					case 3:
						startBound = prevMove + (15 * i);
						endBound = prevMove - (17 * i);
						diff = -16;
						break;
				}

				if (diff > 0) {
					for (int jj = startBound; jj < endBound; jj += diff) {
						if (jj != bestMove && board.validMove(jj)) {
							successors.Add(jj);
						}		
					}
				}
				else {
					for (int jj = startBound; jj > endBound; jj += diff) {
						if (jj != bestMove && board.validMove(jj)) {
							successors.Add(jj);
						}		
					}
				}
			}
		}

		return successors;
	}


	private int[] shuffle(int[] array) {
		int n = array.Length;
		Random rng = new Random();
		while (n > 1) {
			int k  = rng.Next(n--);
			int temp = array[n];
			array[n] = array[k];
			array[k] = temp;
		}

		return array;
	}
}