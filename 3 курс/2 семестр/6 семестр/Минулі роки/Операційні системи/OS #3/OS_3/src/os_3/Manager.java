/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package os_3;

import java.awt.List;
import java.util.ArrayList;
//import java.util.List;

/**
 *
 * @author SichikUA
 */
public class Manager {

    private ArrayList<Process> ram;
    //private List l;
   // private List<Process> ram;
    private int flag;
    private PrQueue pq;
    private myGui mg;

    public Manager(PrQueue pq, myGui mg) {
        flag = 1;
        ram = new ArrayList<Process>(5);
        for (int i = 0 ; i < 5; i++) {
            ram.add(null);
        }
        this.pq = pq;
        this.mg = mg;
    }
    public void myManager() {
        Process bufProc;
        System.out.println(ram.size());
        while ( true ) {
            System.out.println("fuck");
            if ( flag == 1 ) {
                 System.out.println("fuck1");
                for ( int i = 0; i < ram.size(); i ++) {
                    if ( ram.get(i)== null) {
                         System.out.println("fuck2");
                         bufProc = pq.getFromQueue();
                        if ( bufProc == null) {
                            System.out.println("End program");
                            return;
                        }

                        ram.set(i, bufProc );
                        RunProc rp = new RunProc(bufProc.getTime(), ram, i, mg);
                        Thread t = new Thread(rp);
                        Thread.yield();
                       // try{
                        //Thread.sleep(500);
                        //} catch (InterruptedException e) {
                        
                        //}

                        mg.setProgBar(i, bufProc.getSize());
                        t.start();
                        Thread.yield();
                        
                    }
                }
            }
        }
    }

}
