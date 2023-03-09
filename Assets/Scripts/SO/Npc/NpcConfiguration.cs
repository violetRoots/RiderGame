using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using RiderGame.Gameplay;

namespace RiderGame.SO
{
    [CreateAssetMenu(fileName = "Npc_1", menuName = "RiderGame/Npc", order = 3)]
    public partial class NpcConfiguration : ScriptableObject
    {
        public bool IsTrigger => isTrigger;

        [Header("Collision")]
        [SerializeField]
        private bool isTrigger;

        public bool IsDynamicOverlayModeOn => overlayMode == NpcOverlayMode.Dynamic;
        public float DynamicOverlayEdgeOffset => dynamicOverlayEdgeOffset;

        public bool IsStaticOverlayModeOn => overlayMode == NpcOverlayMode.Static;
        public string StaticSortingLayer => staticSortingLayer;

        [Header("Overlay")]
        [SerializeField]
        private NpcOverlayMode overlayMode;

        [AllowNesting]
        [ShowIf(nameof(IsDynamicOverlayModeOn))]
        [SerializeField]
        private float dynamicOverlayEdgeOffset;

        [AllowNesting]
        [ShowIf(nameof(IsStaticOverlayModeOn))]
        [SortingLayer]
        [SerializeField]
        private string staticSortingLayer;

        public List<StateContainer> States => states;
        public State StartState { get; private set; }

        public List<ModifierContainer> Modifiers => modifiers;

        [Header("States")]
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

    public enum NpcOverlayMode
    {
        Dynamic = 0,
        Static = 1
    }
}
