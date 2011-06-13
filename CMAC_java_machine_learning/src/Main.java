package main;

import cmac.CMACTester;

public class Main {

	public static void main(String[] args) {

		CMACTester cmacTester = new CMACTester();
		
		double[] xes = new double[6];
		xes[0]=5.75;
		xes[1]=6.25;
		xes[2]=7.75;
		xes[3]=8.15;
		xes[4]=9.95;
		xes[5]=8.52;
	//	cmacTester.doCustomTest(2,10,4,4,xes);
		
		cmacTester.doTest(3);
	}

}
