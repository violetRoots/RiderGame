using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using RiderGame.Gameplay;

namespace RiderGame.SO
{
    [CreateAssetMenu(fileName = "Npc_1", menuName = "RiderGame/Npc", order = 3)]
    public partial class NpcConfiguration : ScriptableObject
    {
        public float ClampAngle => clampAngle;
        public float MovementSpeed => movementSpeed;
        public float PushForce => pushForce;
        public float PushTime => pushTime;
        public float AgressionRadius => agressionRadius;
        public float AgressionMovementSpeed => agressionMovementSpeed;

        [Header("Movement")]
        [SerializeField]
        private float clampAngle;
        [SerializeField]
        private float movementSpeed = 10.0f;
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

        [Dropdown(nameof(StateNames))]
        [SerializeField]
        private string startStateName;

        [ReorderableList]
        [SerializeField]
        private List<StateContainer> states;

        [ReorderableList]
        [SerializeField]
        private List<ModifierContainer> modifiers;
    }
}
