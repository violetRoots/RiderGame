using UnityEngine;
using RiderGame.Gameplay;

namespace RiderGame.Editor
{
    public class NpcCustomGizmos : MonoBehaviour
    {
        public const float LineLength = 10.0f;

        private Npc? _npc;

        public void SetNpcValue(Npc npc)
        {
            _npc = npc;
        }

        private void OnDrawGizmos()
        {
            if (_npc == null) return;

            var npc = (Npc)_npc;

            if (npc.StateController.TryGetActiveStateAs(out MovableState movableState) && movableState.MovementAnimation != null)
            {
                foreach (var animation in movableState.MovementAnimation.animationsInfo)
                {
                    DrawMovementLine(animation.angle, Color.yellow);
                }

                DrawMovementLine(movableState.DirectionAngle, Color.red);
            }
        }

        private void DrawMovementLine(float angle, Color color)
        {
            var endPos = Quaternion.Euler(0, 0, angle) * Vector3.down * LineLength;

            Gizmos.color = color;
            Gizmos.DrawLine(transform.position, transform.position + endPos);
        }
    }
}
