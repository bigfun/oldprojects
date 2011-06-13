package KoToServer;

import java.io.*;
import java.net.*;
import java.util.*;
import java.util.logging.Level;
import java.util.logging.Logger;

public class KotoServer {

    private ServerSocket sock;
    public static int port;
    public static HashMap<java.lang.Integer, Socket> loggedin;
    public static HashMap<java.lang.Integer, User> users;

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
            new KotoServer(port_);
        }
        new KotoServer(9021);
    }

    public KotoServer(int port_) {

        KotoServer.port = port_;
        System.out.println("Server initializing...");
        try {
            sock = new ServerSocket(port_);
        } catch (IOException e) {
            System.out.println("Unable to create server on port " + port_ + " - " + e.getMessage());
            return;
        }

        loggedin = new HashMap<java.lang.Integer, Socket>();
        users = new HashMap<java.lang.Integer, User>();
        Scanner reader;
        String line;
        File file = new File("users.txt");
        try {
            reader = new Scanner(file);
            while (reader.hasNextLine()) {
                line = reader.nextLine();
                System.out.println("line: " + line);
                String[] data = line.split(":", 2);
                System.out.println("data[0]: " + data[0] + " data[1]: " + data[1]);
                users.put(Integer.parseInt(data[0]), User.createUser(Integer.parseInt(data[0]), data[1]));
            }
            reader.close();
        } catch (FileNotFoundException e) {
            System.out.println("users database file not found. Creating new one...");
            try {
                file.createNewFile();
            } catch (IOException ex) {
                Logger.getLogger(KotoServer.class.getName()).log(Level.SEVERE, null, ex);
                System.out.println("Unable to create users database file. Reason: " + e.getMessage());
                System.out.println("Shutting down.");
                return;
            }
        }

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

    public synchronized static void saveUsers() {
        FileOutputStream fop;
        File file = new File("users.txt");
        try {
            file.delete();
            fop = new FileOutputStream(file);
        } catch (FileNotFoundException ex) {
            Logger.getLogger(KotoServer.class.getName()).log(Level.SEVERE, null, ex);
            System.out.println("Oops! Strange thing happened!");
            return;
        }
        for (User user : KotoServer.users.values()) {
            try {
                fop.write(user.toString().getBytes());
                fop.write("\n".getBytes());
            } catch (IOException ex) {
                Logger.getLogger(KotoServer.class.getName()).log(Level.SEVERE, null, ex);
                System.out.println("error happened during saving the database file");
                return;
            }
        }
        try {
            fop.flush();
            fop.close();
        } catch (IOException ex) {
            Logger.getLogger(KotoServer.class.getName()).log(Level.SEVERE, null, ex);
        }

    }

    public synchronized static boolean sendMsg(int userId_, String msg) {
        if (KotoServer.loggedin.containsKey(userId_)) {
            Socket s = (Socket) KotoServer.loggedin.get(userId_);
            try {
                PrintWriter out = new PrintWriter(new BufferedWriter(new OutputStreamWriter(s.getOutputStream())), true);
                out.println(msg);
                System.out.println("i sent to " + userId_ + " Message: " + msg.split(":",3)[2]);
                return true;
            } catch (IOException e) {
            }
        } else {
            return false;
        }
        return false;
    }
}
