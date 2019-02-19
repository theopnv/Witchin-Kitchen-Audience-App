using System;
using System.Collections.Generic;

namespace audience.messages
{
    public class Command
    {
        // Emitted
        public const string JOIN_GAME = "joinGame";
        public const string DISCONNECT = "disconnect";
        public const string SEND_VOTE = "vote";

        // Received
        public const string MESSAGE = "message";
        public const string GAME_QUIT = "gameQuit";
        public const string EVENT_LIST = "eventList";
    }

    public enum Code
    {
        join_game_success = 240,
        join_game_error = 241,

        success_vote_accepted = 270,
        error_vote_didnt_pass = 271,
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

    public class PollChoices
    {
        public List<Event> events;
        public string deadline;
    }

    public class Event
    {
        public int id;
        public int votes;
    }

    public class GameOutcome
    {
        public bool gameFinished;
        public Player winner;
    }
}

