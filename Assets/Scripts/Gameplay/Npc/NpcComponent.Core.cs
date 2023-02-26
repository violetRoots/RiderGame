using UnityEngine;
using RiderGame.SO;

namespace RiderGame.Gameplay
{
    public partial class NpcComponent
    {
        private bool NpcConfigNotNull => value.npcConfiguration != null;

        private void Awake()
        {
            InitNpc();
        }

        private void OnDrawGizmos()
        {
            if (Application.isPlaying) return;

            InitNpc();
        }

        private void InitNpc()
        {
            if (!NpcConfigNotNull)
            {
                ResetControllers();
                return;
            }

            ValidateStateController(value.npcConfiguration);
            ValidateModifierController(value.npcConfiguration);
        }

        private void ResetControllers()
        {
            value.StateController = null;
            value.statesCount = 0;

            value.ModifierController = null;
            value.modifiersCount = 0;
        }
    }
}
