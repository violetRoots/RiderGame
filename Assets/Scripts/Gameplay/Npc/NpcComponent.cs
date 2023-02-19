using System;
using UnityEngine;
using Voody.UniLeo;
using RiderGame.SO;

namespace RiderGame.Gameplay
{
    public class NpcComponent : MonoProvider<Npc> { }

    [Serializable]
    public struct Npc
    {
        public NpcConfiguration enemyConfiguration;

        [Space(10)]
        public SpriteRenderer agressionIcon;
        public SpriteRenderer stunnedIcon;

        [HideInInspector]
        public EnemyState state;
    }

    public enum EnemyState
    {
        Normal,
        Agressive,
        Stunned
    }
}
