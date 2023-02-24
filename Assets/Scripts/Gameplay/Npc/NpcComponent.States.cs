using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using RiderGame.SO;

namespace RiderGame.Gameplay
{
    public partial class NpcComponent
    {
        private void ValidateStateController(NpcConfiguration npcConfiguration)
        {
            value.StateController = new StateRuntimeController(npcConfiguration.States, npcConfiguration.StartState);

            value.statesCount = value.StateController.GetCount();
        }
    }

    public partial struct Npc
    {
        private bool StateControllerValid => StateController != null;

        [Header("STATES")]
        [AllowNesting]
        [ReadOnly]
        public int statesCount;

        private bool HasAggressionState => StateControllerValid && StateController.Has<AggressionState>();
        [AllowNesting]
        [ShowIf(nameof(HasAggressionState))]
        public AggressionStateRefs aggressionStateRefs;

        private bool HasStunnedState => StateControllerValid && StateController.Has<StunnedState>();
        [AllowNesting]
        [ShowIf(nameof(HasStunnedState))]
        public StunnedStateInfo stunnedState;
    }
}
