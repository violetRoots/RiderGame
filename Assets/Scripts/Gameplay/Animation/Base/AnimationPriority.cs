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
        Idle = 0,
        Walk = 10,
        Aggression = 20,
        Stunned = 30
    }
}
