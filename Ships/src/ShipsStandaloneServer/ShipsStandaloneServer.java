/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package ShipsStandaloneServer;
import java.io.*;
import java.net.*;
import java.util.*;
/**
 *
 * @author bigfun
 */
public class ShipsStandaloneServer {
    private ServerSocket sock;
    public static int port;
    public static HashMap<String, Player> players;
    public static HashMap<Integer, Match> games;
    public static ServerOutput log;
    public static PriorityQueue<Integer> queue;
    public static SortedSet<Integer> sortedset;

    public static void main(String[] args) throws IOException {
        int port_;
        if (args.length > 0)
        {
            try {
            port_ = Integer.parseInt(args[0]);
            } catch (NumberFormatException e)
            {
                System.out.println("You have to provide valid port (range 1024-65535)");
                return;
            }
            if (port_ < 1024)
            {
                System.out.println("The valid range for port is 1024-65535");
                return;
            }
            new ShipsStandaloneServer(port_);
        }
        new ShipsStandaloneServer(9021);
    }
    public ShipsStandaloneServer(int port_) {
        ShipsStandaloneServer.port = port_;
        System.out.println("Server initializing...");
        try {
            sock = new ServerSocket(port_);
        } catch (IOException e) {
            System.out.println("Unable to create server on port " + port_ + " : " + e.getMessage());
            return;
        }
        players = new HashMap<String, Player>();
        games = new HashMap<Integer, Match>();
        sortedset = new TreeSet<Integer>();
        log = new ServerOutput();
        System.out.println("Server initialized. Waiting for requests... ");
        while (true) {
            Socket socket;
            try {
                socket = sock.accept();
            } catch (IOException e) {
                System.out.println("Exception during serving the client. Message: " + e.getMessage());
                break;
            }
            new ServeClient(socket);
            System.out.println("Serving the client, new ServeClient Object Created...");
        }
        //zamykamy gniazdo
        try {
            sock.close();
        } catch (IOException e) {
        }
        }
}
