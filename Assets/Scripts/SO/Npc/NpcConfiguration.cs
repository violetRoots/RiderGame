using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using RiderGame.Gameplay;

namespace RiderGame.SO
{
    [CreateAssetMenu(fileName = "Npc_1", menuName = "RiderGame/Npc", order = 3)]
    public partial class NpcConfiguration : ScriptableObject
    {
        public List<StateContainer> States => states;
        public State StartState { get; private set; }

        public List<ModifierContainer> Modifiers => modifiers;

        public float PushForce => pushForce;
        public float PushTime => pushTime;
        public float AgressionRadius => agressionRadius;
        public float AgressionMovementSpeed => agressionMovementSpeed;

        [Header("Agression Mode")]
        [SerializeField]
        private float agressionRadius = 3.0f;
        [SerializeField]
        private float agressionMovementSpeed = 10.0f;

        [Header("Collision")]
        [SerializeField]
        private float pushForce = 1.0f;
        [SerializeField]
        private float pushTime = 0.1f;

        [AllowNesting]
        [ReadOnly]
        [SerializeField]
        private string startStateName;

        [ReorderableList]
        [SerializeField]
        private List<StateContainer> states;

        [ShowIf(nameof(HasStateDublicates))]
        [TextArea]
        [ReadOnly]
        [InfoBox("States has dublicates", EInfoBoxType.Warning)]
        [SerializeField]
        private string hasStateDublicatesMessage;


        [ReorderableList]
        [SerializeField]
        private List<ModifierContainer> modifiers;

        [ShowIf(nameof(HasModifierDublicates))]
        [TextArea]
        [ReadOnly]
        [InfoBox("Modifiers has dublicates", EInfoBoxType.Warning)]
        [SerializeField]
        private string hasModifierDublicatesMessage;

        [Button("Update")]
        private void Update() => OnValidate();
    }
}
