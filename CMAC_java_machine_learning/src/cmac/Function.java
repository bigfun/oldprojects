package cmac;

public class Function {

	/**
	 * Returns value of sin function for specified arguments
	 * 
	 * @param amplitude
	 * @param frequency
	 * @param phase
	 * @param x
	 * @return
	 */
	public double sin(double amplitude, double frequency, double phase, double x) {
		return amplitude * Math.sin(frequency * x + phase);
	}

	/**
	 * Computes values for specified table of arguments
	 * 
	 * @param amplitude
	 * @param frequency
	 * @param phase
	 * @param x
	 * @return
	 */
	public double[] sinusoid(double amplitude, double frequency, double phase,
			double[] x) {
		double[] y = new double[x.length];

		for (int i = 0; i < x.length; i++) {
			y[i] = this.sin(amplitude, frequency, phase, x[i]);
		}
		return y;
	}
}
