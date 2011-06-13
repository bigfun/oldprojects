/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package Basics;

/**
 *
 * @author bigfun
 */





public class ShipsProtocol {
  public  enum ShipType
{
    CARRIER,
    BATTLESHIP,
    DESTROYER,
    SUBMARINE,
    PATROL_BOAT
};
    public static final String GAME_READY = "Game:ready"; // player is ready to start the Game
    public static final String GAME_END = "Game:end"; // Game has been ended by one of the player
    public static final String GAME_TIMED_OUT = "Game:timedout"; // player timed out
    public static final String GAME_HIT_TRY_ = "Game:hittry:"; // Game:hittry:posx:posy
    public static final String GAME_HIT_MISS = "Game:hitmiss";
    public static final String GAME_HIT_SUCCESS_ = "Game:hitsuccess:";  // Game:hit:success:flooded/alive
    public static final String GAME_ALL_FLOODED = "Game:allflood"; // player won by flooding all ships

    public static final String GAME_MESSAGE_ = "Game:message:";

    public static final String SEP = ":";

    public static final String LOBBY_SEND_MESSAGE_ = "Lobby:sendMessage:";
    public static final String LOBBY_NEW_MESSAGE_ = "Lobby:newMessage:"; // Lobby:newMessage:nick:body
    public static final String LOBBY_REGISTER_ = "Lobby:register:"; // Lobby:register:nick
    public static final String LOBBY_IS_USED = "Lobby:isused"; //some1 already has this nick
    public static final String LOBBY_GET_PLAYERS = "Lobby:getplayers"; // return all players nicknames
    public static final String LOBBY_PLAYERS_ = "Lobby:players:"; // Lobby:players:nickname:nickname:.....
    public static final String LOBBY_GET_GAMES = "Lobby:getGames"; // return  all Games
    public static final String LOBBY_GAMES_ = "Lobby:Games:"; // Lobby:Games:number_of_Games:(Game):(Game):... Game = Gamenumber:hostplater:_empty:enemynick
    public static final String LOBBY_LEFT_ = "Lobby:left:"; // Lobby:left:nickname
    public static final String LOBBY_GAME_CREATED_ = "Lobby:Gamecreated:"; // Lobby:Gamecreated:number:hostplayer:_empty/nick
    public static final String LOBBY_GAME_CHANGED_ = "Lobby:Gamechanged:"; // Lobby:Gamechanged:number:hostplayer/_empty:nick/_empty
    public static final String LOBBY_JOIN_GAME_ = "Lobby:joinGame:"; // Lobby:joinGame:Gamenumber
    public static final String LOBBY_LEAVE_GAME = "Lobby:leaveGame"; // leave Game
    public static final String LOBBY_NEW_PLAYER_ = "Lobby:newPlayer:"; 
    public static final String LOBBY_LEAVE = "Lobby:leave";

    public static final String COMMAND_FAILED = "commandfailed";
    public static final String COMMAND_WRONG = "commandwrong";
    public static final String COMMAND_OK = "commandok";
    public static final String COMMAND_CHECK_ALIVE = "commandcheckalive";
    public static final String COMMAND_IS_ALIVE = "COMMAND_IS_ALIVE";

}
