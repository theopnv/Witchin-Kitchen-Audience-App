﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace audience.game
{
    public static class Events
    {

        public enum EventID
        {
            freezing_rain = 0,
            disco_mania,

            max_id,
        }

        public static Dictionary<EventID, string> EventList = new Dictionary<EventID, string>
        {
            { EventID.freezing_rain, "Freezing Rain"},
            { EventID.disco_mania, "Disco Mania"},
        };

    }

}