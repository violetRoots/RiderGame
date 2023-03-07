using UnityEngine;
using RiderGame.Editor;

namespace RiderGame.Gameplay
{
    [RequireComponent(typeof(NpcCustomGizmos))]
    public partial class NpcComponent
    {
        private bool NpcConfigNotNull => value.npcConfiguration != null;

        private void Awake()
        {
            InitNpc();
            InitNpcCustomGizmos();
        }

        private void OnDrawGizmos()
        {
            if (Application.isPlaying) return;

            InitNpc();
            InitNpcCustomGizmos();
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

        private void InitNpcCustomGizmos()
        {
            var gizmos = GetComponent<NpcCustomGizmos>();
            gizmos.SetNpcValue(value);
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
