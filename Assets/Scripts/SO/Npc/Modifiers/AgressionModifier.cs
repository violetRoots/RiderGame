using System;
using UnityEngine;

namespace RiderGame.Gameplay
{
    public class AgressionModifier : Modifier
    {
        [SerializeField]
        private float duration = 0.0f;

        [SerializeField]
        private bool test = true;
    }
}
