using UnityEngine;
using RiderGame.SO;

namespace RiderGame.Level
{
    public class RuntimeLevelData
    {
        public LevelConfiguration currentLevelConfig;
        public LevelStage currentStage;
        public float processStageTime;
        
        public float currentWorldSpeed;
    }
}