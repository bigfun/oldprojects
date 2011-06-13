/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package ShipsClient;

import java.net.*;
import java.io.*;
import javax.swing.*;

/**
 *
 * @author bigfun
 */
public class GameClient {

    int port;
    String hostname;
    Socket socket;

    public GameClient(int port_, String hostname_) {
        port = port_;
        hostname = hostname_;
    }

    public void startGame() {
        try {
            InetAddress addr = InetAddress.getByName(hostname);
            socket = new Socket(addr, port);
        } catch (UnknownHostException ex) {
            showErrorAndExit("Nie udało się odnaleźć serwera");
            return;
        } catch (IOException ex) {
            showErrorAndExit("Nie udało się połączyć z serwerem");
            return;
        }
        try {
            PlayingView view = new PlayingView(socket);
            view.setVisible(true);
            new ConnHandler(socket, view);
        } catch (Exception ex) {
            StartWindow.getInstance().setVisible(true);
            return;
        }

    }

    private void showErrorAndExit(String error) {
        JOptionPane.showMessageDialog(StartWindow.getInstance(), error, "Błąd", JOptionPane.ERROR_MESSAGE);
        StartWindow.getInstance().setVisible(true);
    }
}
