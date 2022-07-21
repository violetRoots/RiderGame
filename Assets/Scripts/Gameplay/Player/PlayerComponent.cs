using System;
using RiderGame.SO;
using UnityEngine;
using Voody.UniLeo;

namespace RiderGame.Gameplay
{
    public class PlayerComponent : MonoProvider<Player>
    {
        public Player Value => value;
    }

    [Serializable]
    public struct Player
    {
        public CharacterConfiguration character;
        public SpriteRenderer renderer;
    }
}
