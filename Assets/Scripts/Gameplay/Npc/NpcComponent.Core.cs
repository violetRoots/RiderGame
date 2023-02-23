using UnityEngine;
using RiderGame.SO;

namespace RiderGame.Gameplay
{
    public partial class NpcComponent
    {
        private bool NpcConfigNotNull => value.npcConfiguration != null;

        private void OnDrawGizmos()
        {
            if(!NpcConfigNotNull)
            {
                ResetControllers();
                return;
            }

            ValidateStateController(value.npcConfiguration);
            ValidateModifierController(value.npcConfiguration);
        }

        private void ResetControllers()
        {
            value.stateController = null;
            value.statesCount = 0;

            value.modifierController = null;
            value.modifiersCount = 0;
        }
    }
}
