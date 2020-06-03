package os2;

public class Main {

	/**
	 * @param args
	 */
	public static void main(String[] args) {
		
		int delayA = 300;
        int delayB = 100;

        /*
         * 1: білет за 28 коп.;
         * 2: білет за 37 коп.;
         * 3: білет за 50 коп.;
         * 4: білет за 77 коп.;
         * 5: білет за 91 коп.;
         */

        int tasksA[] = {1, 5, 8, 1};
        int tasksB[] = {4, 3, 4,1,1,1,1,1,1,1};
        int countT = tasksA.length + tasksB.length; 

        System.out.println("ПОЧАТОК РОБОТИ:\n");

		Common sM = new Common('W');
		ThreadC C = new ThreadC('C', sM, countT);
		ThreadAB A = new ThreadAB(tasksA, 'A', delayA, sM, delayA);
		ThreadAB B = new ThreadAB(tasksB, 'B', delayB, sM, delayB);

		Thread tC = new Thread(C);
		Thread tA = new Thread(A);
		Thread tB = new Thread(B);
		
		tC.start();
		tA.start();
		tB.start();
		
		try {
			tC.join();
		} catch (InterruptedException e) {
			e.printStackTrace();
		}
		System.out.println("КІНЕЦЬ РОБОТИ");
    }
}

