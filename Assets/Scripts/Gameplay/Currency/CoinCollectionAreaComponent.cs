using System;
using UnityEngine;
using Voody.UniLeo;

namespace RiderGame.Gameplay
{
    public class CoinCollectionAreaComponent : MonoProvider<CoinCollectionArea> { }

    [Serializable]
    public struct CoinCollectionArea
    {
        public Collider2D collider;
    }
}
