﻿using System;
using System.Collections.Generic;

namespace audience.messages
{
    public class Command
    {
        // Socket IO reserved
        public const string CONNECT = "connect";
        public const string DISCONNECT = "disconnect";

        // Emitted
        public const string JOIN_GAME = "joinGame";
        public const string SEND_VOTE = "vote";
        public const string REGISTER_VIEWER = "registerViewer";
        public const string CAST_SPELL_RESPONSE = "castSpell";
        public const string VOTE_FOR_INGREDIENT = "voteForIngredient";

        // Received
        public const string MESSAGE = "message";
        public const string GAME_OUTCOME = "gameOutcome";
        public const string EVENT_LIST = "eventList";
        public const string JOINED_GAME = "joinedGame";
        public const string UPDATED_GAME_STATE = "updateGameState";
        public const string POLL_RESULTS = "pollResults";
        public const string END_GAME = "endGame";
        public const string CAST_SPELL_REQUEST = "castSpellRequest";
        public const string INGREDIENT_RESULTS = "ingredientPollResults";
        public const string STOP_INGREDIENT_POLL = "stopIngredientPoll";
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

        ingredient_vote_accepted = 350,
        ingredient_vote_error = 351,
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
        public string name;
    }

    public class Player
    {
        public int id;
        public string color;
        public string name;
        public int potions;
        public int ingredients;
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
        public Player[] leaderboards;
    }

    public class SpellRequest
    {
        public Viewer targetedViewer;
        public Player fromPlayer;
    }

    public class Spell
    {
        public int spellId;
        public Player targetedPlayer;
        public Viewer caster;
    }

    public class EndGame
    {
        public bool doRematch;
    }

    public class IngredientPoll
    {
        public List<Ingredient> ingredients;
    }

    public class Ingredient
    {
        public int id;
        public int votes;
    }

    public static class SocketInfo
    {
        public const string SUFFIX_ADDRESS = "socket.io/?EIO=4&transport=websocket";
    }
}

