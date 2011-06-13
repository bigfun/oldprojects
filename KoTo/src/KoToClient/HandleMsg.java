
package KoToClient;

import java.io.*;
import java.net.*;
import java.util.*;


public class HandleMsg extends Thread {

    Scanner scanner;
    String str;
    Socket s;
    int userId;

    public HandleMsg(Socket soc)
    {
        s = soc;
        try
        {
            //twozymy obiekt do wczytywania
            scanner = new Scanner(s.getInputStream());
        }
        catch (IOException e)
        {
        }
        start();
    }

    public void run()
    {
        while (scanner.hasNextLine())
        {
            str = scanner.nextLine();
            if (str.startsWith("MSG:"))
            {
                String[] data = str.substring(4).split(":",2 );
                KotoClient.view.handleMessage(data[0], data[1]);
            }
            else
            {
                KotoClient.view.handleOther(str);
            }
        }
        try
        {
            scanner.close();
            s.close();
        }
        catch (IOException e)
        {
        }
    }

}