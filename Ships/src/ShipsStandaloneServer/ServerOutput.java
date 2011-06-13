/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package ShipsStandaloneServer;

/**
 *
 * @author bigfun
 */
public class ServerOutput {
    private String logFile;

    public void ServerOutput(String filename) {
        logFile = filename;
    }

    public void print(String msg) {
        System.out.println(msg);
    }

}
