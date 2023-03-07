using RiderGame.Gameplay;
using UnityEngine;

namespace RiderGame.Editor.CustomGizmos
{
    public class MovementAnimationDrawer : BaseGizmosDrawer
    {
        public const float LineLength = 10.0f;

        private MovementAnimation _animationValue;

        public void UpdateValue(MovementAnimation newValue)
        {
            _animationValue = newValue;
        }

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying)
            {
                _animationValue = GetComponent<PlayerMovementAnimationComponent>().Value;
            }

            if (!_animationValue.drawGizmos) return;

            if (TryGetComponent(out PlayerComponent player))
            {
                _animationValue.animationConfiguration = player.Value.character.WalkAnimationConfigs;
            }

            foreach(var animation in _animationValue.animationConfiguration.animationsInfo)
            {
                var endPos = Quaternion.Euler(0, 0, animation.angle) * Vector3.down * LineLength;

                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(transform.position, transform.position + endPos);
            }
        }
    }
}
