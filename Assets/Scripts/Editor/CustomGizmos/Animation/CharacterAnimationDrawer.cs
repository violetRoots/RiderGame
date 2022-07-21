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

        private Player _playerValue;

        public void UpdateValue(Player newValue)
        {
            _playerValue = newValue;
        }

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying)
            {
                _playerValue = GetComponent<PlayerComponent>().Value;
            }

            foreach(var animation in _playerValue.character.AnimationsInfo)
            {
                var endPos = Quaternion.Euler(0, 0, animation.angle) * Vector3.down * LineLength;

                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(transform.position, transform.position + endPos);
            }
        }
    }
}
