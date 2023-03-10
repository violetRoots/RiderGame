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

            DrawMovementLines(ref npc);
            DrawOverlayLine(ref npc);
        }

        private void DrawOverlayLine(ref Npc npc)
        {
            if (!npc.npcConfiguration.IsDynamicOverlayModeOn) return;

            var xOffset = 5.0f;
            var yPos = transform.position.y + npc.npcConfiguration.DynamicOverlayEdgeOffset;
            Vector3 left = new Vector3(transform.position.x - xOffset, yPos, 0);
            Vector3 right = new Vector3(transform.position.x + xOffset, yPos, 0);

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(left, right);
        }

        private void DrawMovementLines(ref Npc npc)
        {
            if (npc.StateController == null) return;

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
