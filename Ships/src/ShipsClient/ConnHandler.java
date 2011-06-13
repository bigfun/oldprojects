
package ShipsClient;

import java.io.*;
import java.net.*;
import java.util.*;

interface ConnActionListener {
    public void connActionPerformed(String message);
};

class ConnHandler extends Thread  {

    Scanner scanner;
    String str;
    Socket s;
    ConnActionListener listener;
    public ConnHandler(Socket soc,  ConnActionListener listener )
    {
        s = soc;
        this.listener = listener;
        try
        {
            //twozymy obiekt do wczytywania
            scanner = new Scanner(s.getInputStream());
        }
        catch (IOException e)
        {
            System.out.println("ConnHandler interrupted");
            return;
        }
        start();
    }

    public void run()
    {
        while (scanner.hasNextLine())
        {
            str = scanner.nextLine();
            listener.connActionPerformed(str);
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