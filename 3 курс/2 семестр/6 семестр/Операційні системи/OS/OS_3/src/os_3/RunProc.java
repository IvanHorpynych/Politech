/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package os_3;

import java.util.ArrayList;

/**
 *
 * @author SichikUA
 */
public class RunProc implements Runnable {
    private int time;
    private ArrayList<Process> que;
    private int num;
    private myGui mg;
    RunProc(int time, ArrayList<Process> que, int num, myGui mg) {
        this.time  = time;
        this.que = que;
        this.num = num;
        this.mg = mg;
    }

  /*  private void myRun( int time, ArrayList<Process> que, int num) {
        for (int i = 0; i < 100 * time; i ++ ) {
            Thread.yield();
        }
        que.add(num, null);

    }*/
    public void run() {
        System.out.println("Process run");
        //mg.setProgBar(num, que.get(num).getSize());
         for (int i = 0; i < 10000 * time; i ++ ) {
            Thread.yield();
        }
        System.out.println("End Proc   ");
        que.set(num, null);

    }

}
