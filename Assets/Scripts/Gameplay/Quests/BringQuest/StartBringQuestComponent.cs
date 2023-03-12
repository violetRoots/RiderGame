using System;
using UnityEngine;
using Voody.UniLeo;
using RiderGame.SO;
using NaughtyAttributes;

namespace RiderGame.Gameplay
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class StartBringQuestComponent : MonoProvider<StartBringQuest>
    {
        private void OnValidate()
        {
            value.collider = GetComponent<CircleCollider2D>();
        }

        public void SetQuestConfigs(BringQuestConfiguration questConfigs)
        {
            value.questConfigs = questConfigs;
        }
    }

    [Serializable]
    public struct StartBringQuest
    {
        public Collider2D collider;

        [HideInInspector]
        public BringQuestConfiguration questConfigs;
    }
}
