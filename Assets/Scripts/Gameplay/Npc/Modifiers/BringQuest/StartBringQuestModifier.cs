using System;
using UnityEngine;
using NaughtyAttributes;

namespace RiderGame.Gameplay
{
    public class StartBringQuestModifier : Modifier
    {
        public BringQuestConfiguration questConfigs;
    }

    [Serializable]
    public struct BringQuestConfiguration
    {
        public float DistanceValueFromMinMaxSlider => UnityEngine.Random.Range(distance.x, distance.y);

        public NpcComponent targetNpc;

        public float radius;
        [MinMaxSlider(0, 1000)]
        public Vector2 distance;

        public Sprite questIcon;
        public Sprite wayPointIcon;
    }
}
