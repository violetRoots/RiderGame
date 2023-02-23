using UnityEngine;
using NaughtyAttributes;
using RiderGame.SO;

namespace RiderGame.Gameplay
{
    public partial class NpcComponent
    {
        private void ValidateModifierController(NpcConfiguration npcConfiguration)
        {
            value.modifierController = new ModifierRuntimeController(npcConfiguration.Modifiers);
            value.modifiersCount = value.modifierController.GetCount();
        }
    }

    public partial struct Npc
    {
        private bool ModifierControllerValid => modifierController != null;

        [Header("MODIFIERS")]
        [AllowNesting]
        [ReadOnly]
        public int modifiersCount;
    }
}
