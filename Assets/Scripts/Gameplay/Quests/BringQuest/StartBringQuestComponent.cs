using System;
using UnityEngine;
using Voody.UniLeo;
using RiderGame.SO;

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
}
