package KoToServer;

import java.io.*;
import java.net.*;
import java.util.*;

public class ServeClient extends Thread {

    BufferedReader in;
    PrintWriter out;
    String str;
    Socket s;
    int userId;

    public ServeClient(Socket soc) {
        s = soc;
        userId = -1;
        try {
            //twozymy obiekt do wczytywania
            in = new BufferedReader(new InputStreamReader(s.getInputStream()));
            out = new PrintWriter(new BufferedWriter(new OutputStreamWriter(s.getOutputStream())), true);
        } catch (IOException e) {
        }
        start();
    }

    private boolean handleAddNew(String str) {
        String[] data = str.split(":", 3);
        int userId_;
        try {
            userId_ = Integer.parseInt(data[0]);
        } catch (NumberFormatException e) {
            out.println("ERROR:UNCORRECT");
            System.out.println("sent: " + "ERROR:UNCORRECT");
            return false;
        }
        String pass = data[1];
        if (KotoServer.users.containsKey(userId_)) {
            out.println("ERROR:User Already Exists");
            System.out.println("sent: " + "ERROR:User Already Exists");
            return false;

        }
        synchronized(KotoServer.users)
        {
        KotoServer.users.put(userId_, User.createUser(userId_, pass));
        }
        out.println("ADDED");
        System.out.println("new user: " + userId_ + " added.");
        KotoServer.saveUsers();
        return true;

    }

    private boolean handleLogin(String str) {

        String[] data = str.split(":", 3);
        try {
            userId = Integer.parseInt(data[0]);
        } catch (NumberFormatException e) {
            out.println("ERROR:BadUser");
            System.out.println("Unable to login, bad user ID provided");
            userId = -1;
            return false;
        }
        String pass = data[1];
        if (!KotoServer.users.containsKey(userId)) {
            out.println("ERROR:BadUser");
            System.out.println("Unable to login, user does not exist in database");
            userId = -1;
            return false;

        }
        if (!KotoServer.users.get(userId).checkPassword(pass)) {
            out.println("ERROR:WrongPassword");
            System.out.println("Unable to login, bad password for user: " + userId);
            userId = -1;
            return false;
        }
        synchronized (KotoServer.loggedin) {
            KotoServer.loggedin.put(userId, s);
        }
        out.println("LOGGEDIN");
        System.out.println("User " + userId + " logged in");
        if (KotoServer.users.get(userId).hasMessages())
        {
            ArrayList<String> temp = KotoServer.users.get(userId).getMessages();
            for (String msg : temp)
            {
                KotoServer.sendMsg(userId, "MSG:" + msg);
            }
            KotoServer.users.get(userId).clearMessages();
        }
        return true;
    }

    private boolean handleLogout(String str) {
        synchronized (KotoServer.loggedin) {
            if (KotoServer.loggedin.containsKey(userId)) {
                KotoServer.loggedin.remove(userId);
                System.out.println("logged out user: " + userId);
            }
        }
        return true;
    }

    private boolean handleMsg(String str) {
        if (userId <= 0 || !KotoServer.loggedin.containsKey(userId)) {
            out.println("ERROR:NotLoggedIn");
            return false;
        }
        String[] data = str.split(":", 2);
        if (!(data[0].length() > 0)) {
            out.println("ERROR:BadUser");
            System.out.println("Unable to send message, target user does not exist");
            return true;
        }
        int targetId;
        try {
            targetId = Integer.parseInt(data[0]);
        } catch (NumberFormatException e) {
            out.println("ERROR:BadUser");
            System.out.println("Unable to send message, target user does not exist");
            return true;
        }

        if (KotoServer.loggedin.containsKey(targetId)) {
            KotoServer.sendMsg(targetId, "MSG:" + userId + ":" + data[1]);
            System.out.println("sent message from user " + userId + " to " + targetId);
        } else if (KotoServer.users.containsKey(targetId)) {
            KotoServer.users.get(targetId).addMessage(str);
            System.out.println("saved message from user" + userId + " to " + targetId + " for further deliver");
        } else {
            out.println("ERROR:BadUser");
            System.out.println("Unable to send message, target user does not exist");
        }
        return true;

    }

    public void run() {
        while (true) {
            try {
                //wczytujemy co zostalo przyslane
                str = in.readLine();

                if (str.startsWith("ADDNEW:")) {
                    System.out.println("trying to handle Add New User request...");
                    if (!this.handleAddNew(str.substring(7))) {
                        break;
                    }
                    continue;
                }
                if (str.startsWith("LOGIN:")) {
                    System.out.println("trying to handle Login User request...");
                    if (!this.handleLogin(str.substring(6))) {
                        break;
                    }
                    continue;
                }

                if (str.startsWith("MSG:")) {
                    System.out.println("trying to handle send message request...");
                    if (!this.handleMsg(str.substring(4))) {
                        break;
                    }
                    continue;
                }

                if (str.startsWith("LOGOUT")) {
                    System.out.println("trying to log out user: " + userId);
                    this.handleLogout(str);
                    break;

                }

            } catch (IOException e) {
                synchronized (KotoServer.loggedin) {
                    if (KotoServer.loggedin.containsKey(userId)) {
                        KotoServer.loggedin.remove(userId);
                        System.out.println("logged out user: userId");
                    }
                }
            }
        }
        try {
            in.close();
            out.close();
            s.close();
        } catch (IOException e) {
        }
    }
}
