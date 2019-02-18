using System.Collections.Generic;

namespace audience.messages
{
    public class Command
    {
        // Requests
        public const string JOIN_GAME = "joinGame";
        public const string REFRESH_LOBBY = "refreshLobby";

        // Responses
        public const string MESSAGE = "message";
        public const string ROOM_LIST = "roomList";
    }

    public enum Code
    {
        join_game_success = 240,
        join_game_error = 241,

        refresh_lobby_error = 251,
    }

    public class Base
    {
        public Code code;
        public string content;
    }

    public class Game
    {
        public string id;
        public string mainSocketID;
        public List<Player> players;
        public List<string> viewers; // socket IDs
    }

    public class Player
    {
        public string color;
        public string name;
    }

    public class Players
    {
        public List<Player> players;
    }
}