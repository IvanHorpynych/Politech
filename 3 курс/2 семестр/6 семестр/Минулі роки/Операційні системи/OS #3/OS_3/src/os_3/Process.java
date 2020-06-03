/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package os_3;

import java.util.*;
/**
 *
 * @author SichikUA
 */
public class Process {

    private int time;
    private int size;

    public Process(int time, int size){
        this.time = time;
        this.size = size;
    }

    public int getTime(){
        return this.time;
    }
    public int getSize() {
        return this.size;
    }

}

class PrQueue {

    private Queue<Process> queue;

    public PrQueue() {
        queue = new LinkedList<Process>();
      
    }
    public void addToQueue(Process pr) {
        queue.offer(pr);
    }
    public Process getFromQueue() {
        return queue.poll();
    }
    public Process getFromQueueNotDel() {
        return queue.peek();
    }
}
