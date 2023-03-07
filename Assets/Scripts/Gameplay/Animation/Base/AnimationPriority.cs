using UnityEngine;

namespace RiderGame.Gameplay
{
    public enum PlayerAnimationPriority
    {
        Walk = 0,
        Dash = 10,
        ObstacleCollision = 20,
        EnemyCollision = 30
    }

    public enum NpcAnimationPriority
    {
        Walk = 0,
        Aggression = 10,
        Stunned = 20
    }
}
