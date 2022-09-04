using System;
using UnityEngine;
using Voody.UniLeo;
using RiderGame.SO;
using NaughtyAttributes;

namespace RiderGame.Gameplay
{
    public class StartBringQuestComponent : MonoProvider<StartBringQuest> { }

    [Serializable]
    public struct StartBringQuest
    {
        public BringQuestConfiguration questConfigs;

        [Space(10)]
        public Collider2D collider;
    }

    [Serializable]
    public struct BringQuestConfiguration
    {
        public float Distance => UnityEngine.Random.Range(distance.x, distance.y);
        
        public Sprite questIcon;
        public Sprite wayPointIcon;

        [Space(10)]
        public GameObject completeQuestPrefab;
        [MinMaxSlider(0, 1000)]
        public Vector2 distance;
    }
}
