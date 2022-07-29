using System;
using UnityEngine;
using Voody.UniLeo;
using RiderGame.SO;

namespace RiderGame.Gameplay
{
    public class EnemyComponent : MonoProvider<Enemy> { }

    [Serializable]
    public struct Enemy
    {
        public float MovementDirectionAngle
        {
            get => movementDirectionAngle;
            set => movementDirectionAngle = Mathf.Clamp(value, -enemyConfiguration.ClampAngle, enemyConfiguration.ClampAngle);
        }

        public EnemyConfiguration enemyConfiguration;

        private float movementDirectionAngle;
    }
}
