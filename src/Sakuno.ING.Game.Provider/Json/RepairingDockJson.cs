﻿using System;
using Newtonsoft.Json;
using Sakuno.ING.Game.Models;

namespace Sakuno.ING.Game.Json
{
    internal class RepairingDockJson : IRawRepairingDock
    {
        [JsonProperty("api_id")]
        public RepairingDockId Id { get; set; }
        [JsonProperty("api_state")]
        public RepairingDockState State { get; set; }

        public int api_ship_id;
        public ShipId? RepairingShipId => api_ship_id > 0 ? (ShipId?)api_ship_id : null;
        public long api_complete_time;
        public DateTimeOffset CompletionTime => DateTimeOffset.FromUnixTimeMilliseconds(api_complete_time);
        public int api_item1;
        public int api_item3;
        public Materials Consumption => new Materials
        {
            Fuel = api_item1,
            Steel = api_item3
        };
    }
}
