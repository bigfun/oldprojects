package cmac;

public class CMACApproximator {
	public static boolean DEBUG = true;
	public int interval_begging;
	public int interval_ending;
	public int m0;
	public int L;
	private Interval[][] levels;
	private double delta;
	private double length;
	private double[] xes;
	private int[][] active_weights;

	public CMACApproximator(int interval_begging, int interval_ending, int m0,
			int L) {

		if (interval_ending <= interval_begging || m0 == 0 || L == 0) {
			System.err.println("Wrong parameters::terminated");
			System.exit(1);
		}
		this.interval_begging = interval_begging;
		this.interval_ending = interval_ending;
		this.m0 = m0;
		this.L = L;

		this.levels = new Interval[this.L][this.m0];
		this.length = (this.interval_ending - this.interval_begging)
				/ (double) this.m0;
		this.delta = this.length / this.L;

		log("CMAC Approximator:: start");
		log("CMAC Approximator:: number of all weights is: "
				+ (m0 + (L - 1) * (m0 + 1)));

	}

	public void setXes(double[] args) {
		this.xes = args;
		this.active_weights = new int[args.length][0];
	}

	public CMACApproximator() {
		this(2, 10, 4, 4);
	}

	private void prepareLevelZero() {
		levels[0] = new Interval[this.m0];

		for (int i = 0; i < this.m0; i++) {
			this.levels[0][i] = new Interval(
					this.interval_begging + i * length, this.interval_begging
							+ (i + 1) * length);
		}
	}

	private void prepareLevel(int lvl) {
		levels[lvl] = new Interval[this.m0 + 1];

		double newbeg = this.interval_begging + (this.delta * lvl);
		this.levels[lvl][0] = new Interval(this.interval_begging, newbeg);

		for (int i = 1; i < this.m0; i++) {
			this.levels[lvl][i] = new Interval(newbeg + ((i - 1) * length),
					newbeg + (i * length));
		}

		this.levels[lvl][this.m0] = new Interval(this.interval_ending - length
				+ (delta * lvl), this.interval_ending);
	}

	public void doCMAC() {
		log("CMAC Approximator:: CMAC approximation started!");

		this.prepareLevelZero();
		for (int i = 1; i < this.L; i++) {
			this.prepareLevel(i);
		}
		log("CMAC Approximator:: intervals on all " + this.L
				+ " levels has been computed");

		for (int i = 0; i < xes.length; i++) {
			this.active_weights[i] = this.getActiveWeightsForX(xes[i]);
		}
		log("CMAC Approximator:: active weights computed");

		printResult();
		log("CMAC Approximator:: CMAC approximation terminated successfully!");
	}

	private void printResult() {
		if (DEBUG) {
			for (int i = 0; i < xes.length; i++) {
				System.out.println("value of x: " + xes[i]);
				for (int j = 0; j < active_weights[i].length; j++) {
					System.out.print(active_weights[i][j] + ", ");
				}
				System.out.println("");
			}
		}
	}

	private int[] getActiveWeightsForX(double x) {
		int[] act_weights = new int[this.L];
		int act_w = 0;

		for (int i = 0; i < this.levels.length; i++) {
			for (int j = 0; j < this.levels[i].length; j++) {
				if (x <= this.levels[i][j].getEnd()) {
					act_weights[i] = act_w + j;
					break;
				}
			}
			act_w += this.levels[i].length;
		}

		return act_weights;
	}

	private class Interval {
		@SuppressWarnings("unused")
		private double start;
		private double end;

		public Interval(double start, double end) {
			this.start = start;
			this.end = end;
		}

		public double getEnd() {
			return this.end;
		}

	}

	public void log(String str) {
		if (DEBUG) {
			System.out.println(str);
		}
	}

}
