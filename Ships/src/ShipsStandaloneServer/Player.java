/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package ShipsStandaloneServer;

import java.net.*;
import java.io.*;
import java.util.*;

;

/**
 *
 * @author bigfun
 */
public class Player {

    private String nickname;
    private Socket socket;
    private Match currentGame;
    PrintWriter out;

    public Player(String nick, Socket sock) {
        nickname = nick;
        socket = sock;
        try {
            out = new PrintWriter(new BufferedWriter(new OutputStreamWriter(socket.getOutputStream())),true);
        } catch (IOException ex) {
            ShipsStandaloneServer.log.print("ERROR: Socket: " + socket.toString() + " " + ex.getMessage());
        }


    }

    public String getNick() {
        return nickname;
    }

    public void setCurrentGame(Match game) {
        currentGame = game;
    }

    public boolean isPlaying() {
        if (currentGame != null && currentGame.isPlayed()) {
            return true;
        } else {
            return false;
        }
    }

    public void sendMsg(String msg) {
        out.println(msg);
    }
    public Match getCurrentGame()
    {
        return currentGame;
    }
}
