using System;
using System.Globalization;
using System.Diagnostics;

public class MiniMax_IterativeDeepening {
	private Successors successors = Successors.getInstance();
	private Board board = Board.getInstance();
	private int returnMove = 0;
	private int maxDepth = 10;
	private int largePos;
	private int largeNeg;
	private int maxTime;
	private int nodesGen;
	private Stopwatch stopWatch;

	public MiniMax_IterativeDeepening(int maxTime) {
		this.largePos =  (1 << 20) - 1;
		this.largeNeg = -(largePos);
		this.maxTime = maxTime;
		this.nodesGen = 0;
		this.stopWatch = new Stopwatch();
	}


	public int execute(int prevMove) {
		int initDepth = 3;
		int curDepth = initDepth;
		this.stopWatch.Start();

		while (true) {
			nodesGen = 0;
			bool isNewMaxDepth = curDepth > initDepth;
			int[] move = MiniMax(prevMove, largeNeg, largePos, curDepth, isNewMaxDepth);
			double diffTime = this.stopWatch.Elapsed.TotalMilliseconds / 1000.0;
			if (diffTime >= this.maxTime * 1.0) {
				Console.WriteLine("Time's up! Did not reach...");
				String temp2 = String.Format("Ply: {0}, Time: {1:0.0000} sec, Nodes Generated: {2:n0}", curDepth,
					diffTime, nodesGen);
				Console.WriteLine(temp2);
				break;
			}

			String temp = String.Format("Ply: {0}, Time: {1:0.0000} sec, Nodes Generated: {2:n0}", curDepth,
				diffTime, nodesGen);
			Console.WriteLine(temp);
			curDepth += 1;
			returnMove = move[1];
			if (curDepth > maxDepth) {break;}
		}
		this.stopWatch.Stop();
		this.stopWatch.Reset();

		return returnMove;
	}


	public int[] MiniMax(int prevMove, int alpha, int beta, int depth, bool isNewMaxDepth) {
		this.nodesGen += 1;
		if (cutOffTest(depth)) {
			return new int[] {Heuristics.getInstance().getScore(board.getBoard(),
			depth)};
		}

		System.Collections.ArrayList succs;
		if (isNewMaxDepth) {
			succs = successors.getSuccessors2(prevMove, returnMove);
		}
		else {
			succs = successors.getSuccessors(prevMove);
		}

		// If MAX player
		if (board.getMaxTurn()) {
			int[] values = {largeNeg, 0};
			foreach (int successor in succs) {
				board.placeOnBoard(successor);
				int[] results = this.MiniMax(successor, alpha, beta, depth - 1, false);
				board.resetBoard(successor);
				if (results[0] > values[0]) {
					values[0] = results[0];
					values[1] = successor;
				}
				// Alpha beta pruning
				if (values[0] >= beta) {return values;}
				alpha = Math.Max(alpha, values[0]);
			}
			return values;
		}
		// If MIN player
		else {
			int[] values = {largePos, 0};
			foreach (int successor in succs) {
				board.placeOnBoard(successor);
				int[] results = this.MiniMax(successor, alpha, beta, depth - 1, false);
				board.resetBoard(successor);
				
				if (results[0] < values[0]) {
					values[0] = results[0];
					values[1] = successor;
				}
				// Alpha beta pruning
				if (values[0] <= alpha) {return values;}
				beta = Math.Min(beta, values[0]);
			}
			return values;
		}
	}


	public bool cutOffTest(int depth) {
		// Check for time
		if (this.stopWatch.Elapsed.TotalSeconds >= this.maxTime) {
			return true;
		}

		// Check for depth
		if (depth <= 0) return true;

		return board.isTerminalBoard();
	}
}