/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package ShipsStandaloneServer;
import Basics.*;
/**
 *
 * @author bigfun
 */
public class Match {

    private int gameNumber;
    private boolean isPriv;
    private Player[] currentPlayers;
    private boolean isPlay;
    private int boardSize;
    private int[] shipsNumber;

    public Match(int gameNum, boolean isPriv, int size) {
        gameNumber = gameNum;
        this.isPriv = isPriv;
        boardSize = size;
        currentPlayers = new Player[2];
        shipsNumber = new int[5];
        isPlay = false;
    }

    public int getGameNumber() {
        return gameNumber;
    }

    public boolean isPrivate() {
        return isPriv;
    }

    public void setPlayer(int pos, Player player) {
        if (pos == 1 || pos == 0) {
            currentPlayers[pos] = player;
        }
    }
    public String getGameInfo()
    {
        String ret = String.valueOf(gameNumber) + ":" + (currentPlayers[0] == null ? "_empty" : currentPlayers[0].getNick()) +
                ":" + (currentPlayers[1] == null ? "_empty" : currentPlayers[1].getNick());
        return ret;
    }
    public Player getPlayer(int pos) {
        if (pos == 1 || pos == 0) {
           return currentPlayers[pos];
        }
        return null;
    }

    public void setPrivate(boolean value)
    {
        isPriv = value;
    }
    public void setPlayed(boolean value)
    {
        isPlay = value;
    }
    public boolean isPlayed()
    {
        return isPlay;
    }

    public int getSize()
    {
        return boardSize;
    }
    public void setShipNumber(ShipsProtocol.ShipType type, int count)
    {
        shipsNumber[type.ordinal()] = count;
    }

    public int getShipNumber(ShipsProtocol.ShipType type)
    {
        return shipsNumber[type.ordinal()];
    }

    public boolean removePLayer(Player player)
    {
        if (currentPlayers[0] == player)
        {
          currentPlayers[0] = null;
          if (currentPlayers[1] !=null)
          {
              currentPlayers[0] = currentPlayers[1];
              currentPlayers[1] = null;
          }
          return true;
        } else if (currentPlayers[1] == player)
        {
            currentPlayers[1] = null;
            return true;
        }
        return false;
    }
    public boolean isEmpty()
    {
        if (currentPlayers[0] == null && currentPlayers[1] == null)
            return true;
        else
            return false;
    }
}
