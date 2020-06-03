package os2;

	import java.util.Random;
	import java.util.logging.Level;
import java.util.logging.Logger;
	
public class Common {
	/**
	 *  COINS - CONTAIN ALL COINS
	 *  GIVING - CONTAIN POINTER TO CELLS OF COINS, WHICH WAS DECRIMENT
	 *  gNUM - NUMBER OF GIVING COINS
	 *  uNum - NUMBER OF UNIQUE GIVING COINS
	 *  uPos - POSITION OF NEXT UNIQUE COIN
	 */
	
    int[][] coins = {{50, 25, 10, 5, 2, 1}, {1, 10, 15, 20, 25, 50}};
    int randD;
    int delay;
    int task;
    int price;
	
	char name;
	int change = 0;
	boolean set = false;
	
	Common(char name){
		this.name = name;
	}
	
	synchronized void put(int change, int task, int price, char name, int maxD){	
		delay = maxD;
		
		if (set)
			try {
				wait();
			} catch (InterruptedException e) {
				e.printStackTrace();
			}
			
		System.out.println("Процес "+name+" почав завдання "+task+".");	
		this.change = change;
		this.task = task;
		this.price = price;
		System.out.println("	Процес "+name+" сформував здачу у кількості "+change+" коп.");
		System.out.println("Процес "+name+" закінчив завдання "+task+".");
		System.out.println("");
		
		set = true;
		notify();
		del();
	}

	synchronized void work(){
		
		if (!set)
			try {
				wait();
			} catch (InterruptedException e) {
				e.printStackTrace();
			}
		
		count(change, name, task, price, 1000);	
		set = false;
		notify();
	}
	
	//COUNT BANK
    private int bank(){
        int bank = 0;

        for(int i = 0; i < coins[0].length; i++)
            bank += coins[0][i] * coins[1][i];

        return bank;
    }

    //Dec COINS IN BANK
    private void giveCoins(int[] giving, int gNum){
        for(int i = 0; i < gNum; i++){
            this.coins[1][giving[i]]--;
        }
    }

    //SHOW BANK
    private void showBank(){
        System.out.println("    Банк:");
        int j = 0;
        System.out.print("    номінал: ");
        for(j=0; j < 2; j++){
        	if (j == 1) System.out.print("к-сть  : ");
            for(int i = 0; i < coins[0].length; i++){
                System.out.print(coins[j][i]+" ");
                if(coins[j][i] < 10) System.out.print(" ");
            }
             
            if (j == 1) System.out.println("     сума монет:" + bank());
            System.out.println("");
            System.out.print("    "); 
        }

    }

    //CREATE VIRTUAL BANK
    private int[] virtualNum(int[][] coins){
        int[] virt = new int[coins[0].length];

        for(int i = 0; i < coins[0].length; i++)
            virt[i] = coins[1][i];
        return virt;
    }

    //COUNT NUMBER OF USED COINS
    private int num(int coin, int[] giving){
        int num = 0, i;
        
        for(i = 0; i < this.coins[0].length; i++)
            if (this.coins[0][i] == coin) {
                coin = i;
                break;
            }

        for(i = 0; i < giving.length; i++)
            if (giving[i] == coin) {
                if (i != 0 && coin == 0) return num;

                num++;
                if (i == 120 || giving[i+1] != coin) return num;
            }
        return num;
    }

    //MAKE RANDOM
    private int rand(int max){
        Random r = new Random();

        int res = (int) (max * r.nextDouble());
        return res;
    }

    //DELAY
    private void del(){
        randD = rand(delay);

        try {
            Thread.sleep(randD);
        } catch (InterruptedException ex) {
            Logger.getLogger(ThreadAB.class.getName()).log(Level.SEVERE, null, ex);
        }
    }

    //FINISH SOUT
    private void finish (){
        System.out.println("Процес C закінчив завдання " + task+ ". Затримка - " + randD + "\n");
    }

    //MAIN METHOD
    private int count(int change, char name, int task, int price, int delay){
        int giving[] = new int[121];
        int gNum = 0, uNum = 0, uPos = 0;
        int result = change;
        this.delay = delay;


        System.out.println("Процес C почав завдання " + task);
        showBank();

        if (change >= 100){
            System.out.println("Здача неможлива. Розрахована здача дорівнює гриані або більша за неї.\n");
            randD = 0;
            finish();
            return 0;
        }

        if (change <= 0){
            System.out.println("Здача неможлива. Розрахована здача дорівнює нулю або від'ємна.\n");
            randD = 0;
            finish();
            return 0;
        }

        if(bank() < change){
            System.out.println("Здача неможлива. Сума монет менша за решту.\n");
            randD = 0;
            finish();
            return 0;
        }

        int i = 0;
        int[] virtNum = virtualNum(coins);

        i = 0;
        while(i < coins[0].length)
        {

            if (virtNum[i] != 0 && change - coins[0][i] >= 0){
                change -= coins[0][i];
                giving[gNum++] = i;

                if (gNum == 1 || giving[uPos] != giving[gNum-1]) {
                    uPos = gNum - 1;
                    uNum++;
                }
                virtNum[i]--;

                continue;
            }
            if (change == 0) break;
            i++;
        }

        del(); //DELAY
        if (change == 0) {

            giveCoins(giving, gNum);

            System.out.println("Здача видана:");

            System.out.print("    ");
            for(i = 0; i < coins[0].length; i++)
            {               
                if (num(coins[0][i], giving) != 0){
                    if (num(coins[0][i], giving) > 1)
                        System.out.print(num(coins[0][i], giving) + "*");
                    System.out.print(coins[0][i]);
                    if (--uNum != 0)System.out.print(" + ");
                }
            }
            System.out.print(" = " + result + " коп.\n");
            finish();
            return 1;
        }else
        {
            System.out.println("    Здача не можлива. Не вистачає монет.");
            finish();
            return 1;
        }
    }
}
