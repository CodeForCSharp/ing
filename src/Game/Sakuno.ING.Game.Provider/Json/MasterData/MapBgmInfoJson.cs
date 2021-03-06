﻿using Newtonsoft.Json;
using Sakuno.ING.Game.Models.MasterData;

namespace Sakuno.ING.Game.Json.MasterData
{
    public class MapBgmInfoJson : IRawMapBgmInfo
    {
        [JsonProperty("api_id")]
        public int Id { get; set; }

        [JsonProperty("api_moving_bgm")]
        public int MapBgmId { get; set; }

        public int[] api_map_bgm;
        public int[] api_boss_bgm;
        public int NormalBattleDayBgmId => api_map_bgm.ElementAtOrDefault(0);
        public int NormalBattleNightBgmId => api_map_bgm.ElementAtOrDefault(1);
        public int BossBattleDayBgmId => api_boss_bgm.ElementAtOrDefault(0);
        public int BossBattleNightBgmId => api_boss_bgm.ElementAtOrDefault(1);
    }
}
