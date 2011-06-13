/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package ShipsClient;

import java.net.*;
import java.io.*;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.swing.*;
import java.awt.*;

/**
 *
 * @author bigfun
 */
public class GameServer {

    ServerSocket serverSocket;
    int port;
    public GameServer(int port_) {
        port = port_;
    }

    public void startGame()
    {
        try {
            serverSocket = new ServerSocket(port);
            serverSocket.setSoTimeout(10000);
        } catch (IOException e) {
            showErrorAndExit("Nie udało się utworzyć serwera na podanym porcie");
            return;
        }
        Socket socket;

            final JFrame frame;

               frame = new JFrame("Oczekiwanie na gracza");
               frame.getContentPane().add(new JLabel("Oczekiwanie na gracza..."), BorderLayout.CENTER);
               frame.setVisible(true);
               frame.repaint();
               frame.pack();
               frame.validate();
            try {

               socket = serverSocket.accept();
            } catch (SocketTimeoutException ex)
            {
                frame.dispose();
            try {
                serverSocket.close();
            } catch (IOException ex1) {
                Logger.getLogger(GameServer.class.getName()).log(Level.SEVERE, null, ex1);
            }
                showErrorAndExit("Minął limit czasu oczekiwania.");
                return;
            }
            catch (IOException e) {
                frame.dispose();
            try {
                serverSocket.close();
            } catch (IOException ex) {
                Logger.getLogger(GameServer.class.getName()).log(Level.SEVERE, null, ex);
            }
                showErrorAndExit("Błąd podczas obsługi przyjmowania portu");

                return;
            }
               frame.dispose();
            try
            {
                PlayingView view = new PlayingView(socket);
                view.setVisible(true);
                new ConnHandler(socket, view);
            } catch (Exception ex)
            {
                StartWindow.getInstance().setVisible(true);
            try {
                serverSocket.close();
            } catch (IOException ex1) {
                Logger.getLogger(GameServer.class.getName()).log(Level.SEVERE, null, ex1);
            }
                return;
            }
        
        //zamykamy gniazdo
        try {
            serverSocket.close();
        } catch (IOException e) {
        }
    }

    private void showErrorAndExit(String error)
    {
            JOptionPane.showMessageDialog(StartWindow.getInstance(), error, "Błąd", JOptionPane.ERROR_MESSAGE);
            StartWindow.getInstance().setVisible(true);
    }
}



