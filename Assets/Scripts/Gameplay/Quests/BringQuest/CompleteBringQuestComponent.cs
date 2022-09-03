using System;
using UnityEngine;
using Voody.UniLeo;
using RiderGame.SO;

namespace RiderGame.Gameplay
{
    public class CompleteBringQuestComponent : MonoProvider<CompleteBringQuest> { }

    [Serializable]
    public struct CompleteBringQuest
    {
        public Collider2D collider;
    }
}
