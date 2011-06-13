/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package ShipsStandaloneServer;

import java.io.*;
import java.net.*;
import java.util.*;
import java.lang.reflect.*;
import Basics.ShipsProtocol;

/**
 *
 * @author bigfun
 */
public class ServeClient extends Thread {

    private BufferedReader in;
    private PrintWriter out;
    private String str;
    private Socket socket;
    private String nickName;
    private HashMap<String, Player> players;
    private ServerOutput log = ShipsStandaloneServer.log;
    private HashMap<Integer, Match> games = ShipsStandaloneServer.games;
    private SortedSet<Integer> sortedset = ShipsStandaloneServer.sortedset;
    private Player player;

    public ServeClient(Socket soc) {
        socket = soc;
        nickName = "";
        players = ShipsStandaloneServer.players;

        try {
            //twozymy obiekt do wczytywania
            in = new BufferedReader(new InputStreamReader(socket.getInputStream()));
            out = new PrintWriter(new BufferedWriter(new OutputStreamWriter(socket.getOutputStream())), true);
        } catch (IOException e) {
            log.print("Unable to create socket reader for " + socket.toString());
        }
        start();
    }

    @Override
    public void run() {
        Class runClass;
        Method runMethod;
        while (true) {
            try {
                //wczytujemy co zostalo przyslane
                str = in.readLine();
                if (str == null || str.equals(""))
                    continue;
                String[] params = str.split(":");
                if (params.length < 2) {
                    out.println(ShipsProtocol.COMMAND_WRONG);
                }
                try {
                    String classname = "ShipsStandaloneServer.ServeClient" + "$" + params[0];
                    log.print("classname: " + classname + " , method: " + params[1]);
                    //runClass = Class.forName("ShipsStandaloneServer.ServeClient$Lobby");
                   // String a = "ShipsStandaloneServer.ServeClient$Lobby";
                    runClass = Class.forName(classname);

                    if (params.length > 2) {
                        Class[] argTypes = new Class[]{String.class};
                        runMethod = runClass.getDeclaredMethod(params[1], argTypes);
                        Object c = runClass.getConstructors()[0].newInstance(this);
                        runMethod.invoke(c, (Object) params[2]);
                    } else {
                        runMethod = runClass.getDeclaredMethod(params[1]);
                        Object c = runClass.getConstructors()[0].newInstance(this);
                        runMethod.invoke(c);
                    }

                } catch (IndexOutOfBoundsException ex) {
                    out.println(ShipsProtocol.COMMAND_FAILED);
                    log.print("Exception in command handler, message: " + ex.getMessage() + ", ExceptionType: " + ex.getClass().toString());

                } catch (Exception ex) {
                    out.println(ShipsProtocol.COMMAND_WRONG);
                    log.print("Exception in command handler, message: " + ex.getMessage() + ", ExceptionType: " + ex.getClass().toString());
                }
            }
            catch (IOException e) {
                log.print("IOException in ServeClient");
                if (player != null && player.getCurrentGame() != null) {
                    Match match = player.getCurrentGame();
                    match.removePLayer(player);
                    player.setCurrentGame(null);
                    out.println(ShipsProtocol.LOBBY_GAME_CHANGED_ + match.getGameInfo());
                    if (match.isEmpty()) {
                        synchronized (games) {
                            games.remove(match.getGameNumber());
                            sortedset.remove(match.getGameNumber());
                        }
                    }
                }
                synchronized (players) {
                    if (players.containsKey(nickName)) {
                        players.remove(nickName);
                        log.print("user disconnected: " + nickName);
                        sendToAll(ShipsProtocol.LOBBY_LEFT_ + nickName);
                    }

                    nickName = "";
                    player = null;
                    break;
                }
            }
        }
        try {
            in.close();
            socket.close();
            log.print("end of connection");
        } catch (IOException e) {
        }
    }

    private void sendToAll(String msg) {
        for (Player ppl : players.values()) {
            ppl.sendMsg(msg);
        }
    }

    public class Lobby {

        public void register(String nickName) {
            synchronized (players) {
                if (players.containsKey(nickName)) {
                    out.println(ShipsProtocol.LOBBY_IS_USED);
                } else {
                    player = new Player(nickName, socket);
                    players.put(nickName, player);
                    ServeClient.this.nickName = nickName;
                    sendToAll(ShipsProtocol.LOBBY_NEW_PLAYER_ + nickName);
                }
            }
        }

        public void sendMessage(String body) {
            if (!nickName.equals("")) {
                synchronized (players) {
                    sendToAll(ShipsProtocol.LOBBY_NEW_MESSAGE_ + nickName + ":" + body);
                    log.print("printing message: " + body + "from " + nickName);
                }
            } else {
                out.println(ShipsProtocol.COMMAND_WRONG);
            }
        }

        public void getPlayers() {
            String ppl;
            synchronized (players) {
                ppl = join(players.keySet());
            }
            out.println(ShipsProtocol.LOBBY_PLAYERS_ + ppl);
        }

        public void getGames() {
            StringBuilder ret = new StringBuilder();
            synchronized (games) {

                for (int i : games.keySet()) {
                    if (!games.get(i).isPlayed()) {
                        ret.append(i);
                        ret.append(":");
                        ret.append(games.get(i).getPlayer(0).getNick());
                        ret.append(":");
                        ret.append(games.get(i).getPlayer(1) == null ? "_empty" : games.get(i).getPlayer(1).getNick());
                        ret.append(":");
                    }
                }

            }
            ret.deleteCharAt(ret.length() - 1);
            out.println(ShipsProtocol.LOBBY_GAMES_ + ret.toString());
        }

        public void createGame(String sizeTxt) {
            int size;
            Match match;
            try {
                size = Integer.parseInt(sizeTxt);
            } catch (NumberFormatException ex) {
                out.println(ShipsProtocol.COMMAND_FAILED);
                return;
            }
            synchronized (games) {

                if (sortedset.size() == 0) {
                    match = new Match(1, false, size);
                } else {
                    match = new Match(sortedset.last() + 1, false, size);
                }
                sortedset.add(match.getGameNumber());
                games.put(match.getGameNumber(), match);
                player.setCurrentGame(match);
                match.setPlayer(0, player);
            }
            sendToAll(ShipsProtocol.LOBBY_GAME_CREATED_ + match.getGameInfo());
        }

        public void leaveGame() {
            if (player == null || player.getCurrentGame() == null) {
                out.println(ShipsProtocol.COMMAND_FAILED);
            } else {
                Match match = player.getCurrentGame();
                match.removePLayer(player);
                player.setCurrentGame(null);
                sendToAll(ShipsProtocol.LOBBY_GAME_CHANGED_ + match.getGameInfo());
                if (match.isEmpty()) {
                    synchronized (games) {
                        games.remove(match.getGameNumber());
                        sortedset.remove(match.getGameNumber());
                    }
                }
            }
        }

        public void joinGame(String number) {
            int gameNumber;
            try {
                gameNumber = Integer.parseInt(number);
            } catch (NumberFormatException ex) {
                out.println(ShipsProtocol.COMMAND_FAILED);
                return;
            }
            synchronized (games) {
                if (!games.containsKey(gameNumber) || games.get(gameNumber).isPlayed() || games.get(gameNumber).getPlayer(1) != null) {
                    out.println(ShipsProtocol.COMMAND_FAILED);
                    return;
                }

                games.get(gameNumber).setPlayer(1, player);
                player.setCurrentGame(games.get(gameNumber));
                out.println(ShipsProtocol.LOBBY_GAME_CHANGED_ + games.get(gameNumber).getGameInfo());
            }
        }

        public void leave() {
            if (!nickName.equals("")) {
                synchronized (players) {
                    if (players.containsKey(nickName)) {
                        players.remove(nickName);
                        log.print("user disconnected: " + nickName);

                    }
                }
                sendToAll(ShipsProtocol.LOBBY_LEFT_ + nickName);
                nickName = "";
                player = null;
                out.println(ShipsProtocol.COMMAND_OK);
            }
            else
                out.println(ShipsProtocol.COMMAND_FAILED);
        }

        private String join(Set<String> strings) {
            StringBuilder ret = new StringBuilder();
            for (String str : strings) {
                ret.append(str);
                ret.append(":");
            }
            ret.deleteCharAt(ret.length() - 1);
            return ret.toString();
        }
    }

    public class Game {

        public void ready() {
            log.print("Game:ready");
        }

        public void end() {
            log.print("game:end");
        }
    }
}
