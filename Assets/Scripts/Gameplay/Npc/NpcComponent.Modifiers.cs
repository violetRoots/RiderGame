using UnityEngine;
using NaughtyAttributes;
using RiderGame.SO;

namespace RiderGame.Gameplay
{
    public partial class NpcComponent
    {
        private void ValidateModifierController(NpcConfiguration npcConfiguration)
        {
            value.ModifierController = new ModifierRuntimeController(npcConfiguration.Modifiers);
            value.modifiersCount = value.ModifierController.GetCount();
        }
    }

    public partial struct Npc
    {
        private bool ModifierControllerValid => ModifierController != null;

        [Header("MODIFIERS")]
        [AllowNesting]
        [ReadOnly]
        public int modifiersCount;
    }
}
