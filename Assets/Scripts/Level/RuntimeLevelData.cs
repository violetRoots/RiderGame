using UnityEngine;
using RiderGame.SO;

namespace RiderGame.Level
{
    public class RuntimeLevelData
    {
        public LevelConfiguration CurrentLevelConfig { get; set; }
        public float ProcessStageTime { get; set; }

        public float CurrentWorldSpeed { get; set; }
    }
}