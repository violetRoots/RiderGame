using RiderGame.Gameplay;
using RiderGame.Inputs;
using RiderGame.Level;
using RiderGame.SO;
using UnityEngine;
using Input = RiderGame.Inputs.Input;

namespace RiderGame.Editor.CustomGizmos
{
    public class CharacterAnimationDrawer : BaseGizmosDrawer
    {
        public const float LineLength = 10.0f;

        private CharacterAnimation _characterAnimationValue;

        public void UpdateValue(CharacterAnimation newValue)
        {
            _characterAnimationValue = newValue;
        }

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying)
            {
                _characterAnimationValue = GetComponent<CharacterAnimationComponent>().Value;
            }

            foreach(var animation in _characterAnimationValue.character.AnimationsInfo)
            {
                var endPos = Quaternion.Euler(0, 0, animation.angle) * Vector3.down * LineLength;

                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(transform.position, transform.position + endPos);
            }
        }
    }
}
