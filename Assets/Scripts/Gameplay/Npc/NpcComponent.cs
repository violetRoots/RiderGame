using System;
using System.Runtime.InteropServices;
using UnityEngine;
using Voody.UniLeo;
using RiderGame.SO;

namespace RiderGame.Gameplay
{
    public partial class NpcComponent : MonoProvider<Npc> { }

    [Serializable]
    [StructLayout(LayoutKind.Auto)]
    public partial struct Npc
    {
        public NpcConfiguration npcConfiguration;

        [Header("RUNTIME DATA")]
        public StateRuntimeController StateController;
        public ModifierRuntimeController ModifierController;
    }
}
