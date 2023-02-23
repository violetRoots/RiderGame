using System;
using System.Collections.Generic;
using UnityEngine;
using RiderGame.Gameplay;

namespace RiderGame.SO
{
    [Serializable]
    public class ModifierRuntimeController : ContainerElementsController<ModifierContainer, Modifier>
    {
        public ModifierRuntimeController(List<ModifierContainer> containers) : base(containers)
        {

        }
    }
}
