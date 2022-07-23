using System;
using UnityEngine;
using Voody.UniLeo;

namespace RiderGame.Gameplay
{
    public class CoinComponent : MonoProvider<Coin>
    {
        public Coin Value => value;
    }

    [Serializable]
    public struct Coin
    {
        public CircleCollider2D collider;
    }
}
