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
            value.stateController = new StateRuntimeController(npcConfiguration.States, npcConfiguration.StartState);

            value.statesCount = value.stateController.GetCount();
        }
    }

    public partial struct Npc
    {
        private bool StateControllerValid => stateController != null;

        [Header("STATES")]
        [AllowNesting]
        [ReadOnly]
        public int statesCount;

        private bool HasAggressionState => StateControllerValid && stateController.Has<AggressionState>();
        [AllowNesting]
        [ShowIf(nameof(HasAggressionState))]
        public AggressionStateInfo aggressionState;

        private bool HasStunnedState => StateControllerValid && stateController.Has<StunnedState>();
        [AllowNesting]
        [ShowIf(nameof(HasStunnedState))]
        public StunnedStateInfo stunnedState;
    }
}
