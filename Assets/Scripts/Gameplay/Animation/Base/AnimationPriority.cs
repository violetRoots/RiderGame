using UnityEngine;

namespace RiderGame.Gameplay
{
    public enum CharacterAnimationPriority
    {
        Movement = 0,
        Dash = 10,
        ObstacleCollision = 20,
        EnemyCollision = 30
    }
}
