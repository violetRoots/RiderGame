using System;
using UnityEngine;
using NaughtyAttributes;
using Voody.UniLeo;
using RiderGame.SO;

namespace RiderGame.Gameplay
{
    public class NpcComponent : MonoProvider<Npc> { }

    [Serializable]
    public struct Npc
    {
        private bool IsConfigValid => enemyConfiguration != null;

        public NpcConfiguration enemyConfiguration;


        [HideInInspector]
        public EnemyState state;

        private bool ShowAggressionState => IsConfigValid && enemyConfiguration.HasState<AggressionState>();
        [AllowNesting]
        [ShowIf(nameof(ShowAggressionState))]
        public AggressionStateInfo aggressionState;

        private bool ShowStunnedState => IsConfigValid && enemyConfiguration.HasState<StunnedState>();
        [AllowNesting]
        [ShowIf(nameof(ShowStunnedState))]
        public StunnedStateInfo stunnedState;
    }

    public enum EnemyState
    {
        Normal,
        Agressive,
        Stunned
    }
}
