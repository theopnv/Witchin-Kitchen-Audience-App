using System.Collections.Generic;

namespace audience.messages
{
    public class Command
    {
        // Requests
        public const string JOIN_GAME = "joinGame";
        public const string DISCONNECT = "disconnect";

        // Responses
        public const string MESSAGE = "message";
        public const string GAME_QUIT = "gameQuit";
    }

    public enum Code
    {
        join_game_success = 240,
        join_game_error = 241,
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