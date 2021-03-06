/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

/*
 * StartWindow.java
 *
 * Created on 2010-01-22, 01:51:11
 */
package ShipsClient;

import javax.swing.JOptionPane;
import java.net.*;
import java.io.*;

/**
 *
 * @author bigfun
 */
public class StartWindow extends javax.swing.JFrame {

    /** Creates new form StartWindow */
    static javax.swing.JFrame instance;

    public StartWindow() {
        instance = this;
        initComponents();
    }

    /** This method is called from within the constructor to
     * initialize the form.
     * WARNING: Do NOT modify this code. The content of this method is
     * always regenerated by the Form Editor.
     */
    @SuppressWarnings("unchecked")
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents() {

        btnCreateServer = new javax.swing.JButton();
        btnJoinGame = new javax.swing.JButton();
        btnConnectServer = new javax.swing.JButton();
        btnExit = new javax.swing.JButton();

        setDefaultCloseOperation(javax.swing.WindowConstants.EXIT_ON_CLOSE);
        setTitle("Okręty");

        btnCreateServer.setText("Stwórz serwer");
        btnCreateServer.setName("btnCreateServer"); // NOI18N
        btnCreateServer.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                btnCreateServerActionPerformed(evt);
            }
        });

        btnJoinGame.setText("Dołącz do gry");
        btnJoinGame.setName("btnJoinGame"); // NOI18N
        btnJoinGame.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                btnJoinGameActionPerformed(evt);
            }
        });

        btnConnectServer.setText("Połącz się z serwerem");
        btnConnectServer.setName("btnConnectServer"); // NOI18N
        btnConnectServer.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                btnConnectServerActionPerformed(evt);
            }
        });

        btnExit.setText("Wyjdź");
        btnExit.setName("btnExit"); // NOI18N
        btnExit.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                btnExitActionPerformed(evt);
            }
        });

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(btnCreateServer, javax.swing.GroupLayout.PREFERRED_SIZE, 192, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(btnJoinGame, javax.swing.GroupLayout.PREFERRED_SIZE, 192, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(btnConnectServer)
                    .addComponent(btnExit, javax.swing.GroupLayout.PREFERRED_SIZE, 192, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addContainerGap(20, Short.MAX_VALUE))
        );

        layout.linkSize(javax.swing.SwingConstants.HORIZONTAL, new java.awt.Component[] {btnConnectServer, btnCreateServer, btnExit, btnJoinGame});

        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addComponent(btnCreateServer)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(btnJoinGame)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(btnConnectServer)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, 37, Short.MAX_VALUE)
                .addComponent(btnExit)
                .addContainerGap())
        );

        pack();
    }// </editor-fold>//GEN-END:initComponents

    private void btnExitActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_btnExitActionPerformed
        System.exit(0);
    }//GEN-LAST:event_btnExitActionPerformed
    public static javax.swing.JFrame getInstance() {
        if (instance != null) {
            return instance;
        } else {
            return new StartWindow();
        }
    }
    private void btnCreateServerActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_btnCreateServerActionPerformed
        String answer = JOptionPane.showInputDialog(rootPane, "Podaj numer portu dla serwera", "Port dla serwera", JOptionPane.QUESTION_MESSAGE);
        if (answer == null) {
            return;
        }
        int port;
        try {
            port = Integer.parseInt(answer);
        } catch (NumberFormatException ex) {
            JOptionPane.showMessageDialog(rootPane, "Podałeś nieporawny numer portu.", "Błąd", JOptionPane.ERROR_MESSAGE);
            return;
        }
        this.setVisible(false);
        GameServer gServer = new GameServer(port);
        gServer.startGame();
    }//GEN-LAST:event_btnCreateServerActionPerformed

    private void btnJoinGameActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_btnJoinGameActionPerformed
        int port;
        String answer = JOptionPane.showInputDialog(rootPane, "Podaj komputer docelowy w postaci nazwahosta:port",
                "Komputer docelowy", JOptionPane.QUESTION_MESSAGE);
        if (answer == null) {
            return;
        }
        String[] ret = answer.split(":");
        if (ret.length < 2) {
            JOptionPane.showMessageDialog(rootPane, "Podałeś niepoprawny adres", "Błąd", JOptionPane.ERROR_MESSAGE);
            return;
        }
        try {
            port = Integer.parseInt(ret[1]);
        } catch (NumberFormatException ex) {
            JOptionPane.showMessageDialog(rootPane, "Podałeś nieporawny numer portu.", "Błąd", JOptionPane.ERROR_MESSAGE);
            return;
        }
        this.setVisible(false);
        GameClient gClient = new GameClient(port, ret[0]);
        gClient.startGame();

    }//GEN-LAST:event_btnJoinGameActionPerformed

    private void btnConnectServerActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_btnConnectServerActionPerformed
        int port;
        String hostname;
        String answer = JOptionPane.showInputDialog(rootPane, "Podaj adres serwera w postaci nazwahosta:port",
                "Serwer", JOptionPane.QUESTION_MESSAGE);
        if (answer == null) {
            return;
        }
        String[] ret = answer.split(":");
        if (ret.length < 2) {
            JOptionPane.showMessageDialog(rootPane, "Podałeś niepoprawny adres", "Błąd", JOptionPane.ERROR_MESSAGE);
            return;
        }
        try {
            port = Integer.parseInt(ret[1]);
        } catch (NumberFormatException ex) {
            JOptionPane.showMessageDialog(rootPane, "Podałeś nieporawny numer portu.", "Błąd", JOptionPane.ERROR_MESSAGE);
            return;
        }
        hostname = ret[0];
        answer = JOptionPane.showInputDialog(rootPane, "Podaj nick na serwerze", "Nick", JOptionPane.QUESTION_MESSAGE);
        if (answer == null || answer.isEmpty()) {
            JOptionPane.showMessageDialog(rootPane, "Musisz podać nick", "Błąd", JOptionPane.ERROR_MESSAGE);
            return;
        }
        Socket socket;
        try {
            InetAddress addr = InetAddress.getByName(hostname);
            socket = new Socket(addr, port);
        } catch (UnknownHostException ex) {
            JOptionPane.showMessageDialog(rootPane, "Nie udało się odnaleźć serwera", "Błąd", JOptionPane.ERROR_MESSAGE);
            return;
        } catch (IOException ex) {
            JOptionPane.showMessageDialog(rootPane, "Nie udało się odnaleźć serwera", "Błąd", JOptionPane.ERROR_MESSAGE);
            return;
        
        }
        this.setVisible(false);
        LobbyView lView = new LobbyView(socket, answer);
        lView.setVisible(true);
    }//GEN-LAST:event_btnConnectServerActionPerformed

    /**
     * @param args the command line arguments
     */
    public static void main(String args[]) {
        java.awt.EventQueue.invokeLater(new Runnable() {

            public void run() {
                new StartWindow().setVisible(true);
            }
        });
    }
    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JButton btnConnectServer;
    private javax.swing.JButton btnCreateServer;
    private javax.swing.JButton btnExit;
    private javax.swing.JButton btnJoinGame;
    // End of variables declaration//GEN-END:variables
}
