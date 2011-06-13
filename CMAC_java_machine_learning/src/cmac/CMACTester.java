package cmac;

public class CMACTester {
	
	private CMACApproximator cmacApp;
	
	public CMACTester() {
		
	}
	
	public void doTest(int number){
		
		switch(number){
			case 1:
				doCustomTest(2,10,4,4,prepareArguments(0.4789,8,3.1415));
				break;
			case 2:
				doCustomTest(-100,100,120,8,prepareArguments(0.4789,24,-3.1415));
				break;
			case 3:
				doCustomTest(0,2000,80,16,prepareArguments(4.5,340,350));
				break;
			default:break;	
		}
	}
	
	public void doCustomTest(int min, int max, int intervals, int levels, double[] xes){
		this.cmacApp = new CMACApproximator(min,max,intervals,levels);
		this.cmacApp.setXes(xes);
		cmacApp.doCMAC();
	}
	
	private double[] prepareArguments(double delta, int how_many, double lowest){
		int count=0;
		double[] xes= new double[how_many];
		
		while(count<how_many){
			xes[count] = lowest+count*delta;
			count++;
		}
		return xes;
	}
	
}
