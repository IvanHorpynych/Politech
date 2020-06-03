package os2;

public class ThreadAB implements Runnable{
	char name;
	Common sM;
	int i = 20;
    int tasks[];
    int delay;
    int maxD;
	
	ThreadAB(int tasks[], char name, int delay, Common sM, int maxD){
		this.name = name;
		this.sM = sM;
		this.tasks = new int[tasks.length];
        this.maxD = maxD;        

        for(int i = 0; i < tasks.length; i++)
            this.tasks[i] = tasks[i];
	}
	
	//RETURN COST OF TICKET
    private int getTicket(int task){
        switch(task)
        {
            case 1: return 28;
            case 2: return 37;
            case 3: return 50;
            case 4: return 77;
            case 5: return 91;
        }
        return 0;
    }

    //COUNT CHANGE FROM TICKET
    private int countChange(int ticketId){
        int price = getTicket(ticketId);
        if (price == 0)
            return 0;
        return 100 - price;
    }

    public void run(){
        int change ;

        for(int i = 0; i < tasks.length; i++)
        {
            change = countChange(tasks[i]);
            if (maxD < 100) maxD = 100;
            sM.put(change, tasks[i], 100 - change, name, maxD);
           try {
			Thread.sleep(500);
		} catch (InterruptedException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
        }
    }
}
