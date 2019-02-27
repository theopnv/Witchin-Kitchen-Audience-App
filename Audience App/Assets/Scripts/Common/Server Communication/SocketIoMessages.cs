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
        public const string REGISTER_VIEWER = "registerViewer";
        public const string CAST_SPELL = "castSpell";

        // Received
        public const string MESSAGE = "message";
        public const string GAME_QUIT = "gameQuit";
        public const string EVENT_LIST = "eventList";
        public const string JOINED_GAME = "joinedGame";
    }

    public enum Code
    {
        join_game_error = 241,

        success_vote_accepted = 270,
        error_vote_didnt_pass = 271,

        register_viewer_success = 300,
        register_viewer_error = 301,

        spell_casted_success = 290,
        spell_casted_error = 291,
    }

    public class Base
    {
        public Code code;
        public string content;
    }

    public class Game
    {
        public string id;
        public string pin;
        public string mainSocketID;
        public List<Player> players;
        public List<Viewer> viewers;
    }

    public class Viewer
    {
        public string socketId;
        public string color;
        public string name;
    }

    public class Player
    {
        public int id;
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
        public int duration;
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

    public class Spell
    {
        public int spellId;
        public Player targetedPlayer;
    }
}

