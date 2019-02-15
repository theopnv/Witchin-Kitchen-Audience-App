using System.Collections.Generic;

namespace audience.lobby.messages
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
        public User User;
        public Code Code;
        public string Content;
    }

    public class User
    {
        public string Name;
    }

    public class LobbyGames
    {
        public List<string> Games;
    }
}