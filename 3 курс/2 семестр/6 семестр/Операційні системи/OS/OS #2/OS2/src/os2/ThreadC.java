package os2;

public class ThreadC implements Runnable{
	char name;
	Common sM;
	int num;
	
	ThreadC(char name, Common sM, int count){
		this.name = name;
		this.sM = sM;
		num = count;
	}
	
	public void run(){
		while(num != 0) {
			sM.work();
			num--;
		}
	}
}
