using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using NaughtyAttributes;
using RiderGame.Gameplay;

namespace RiderGame.SO
{
    [Serializable]
    public class StateRuntimeController : ContainerElementsController<StateContainer, State>
    {
        [AllowNesting]
        [ReadOnly]
        [SerializeField]
        private State startState;

        [AllowNesting]
        [ReadOnly]
        [SerializeField]
        private State currentState;

        public StateRuntimeController(List<StateContainer> containers, State start) : base(containers)
        {
            if (start != null)
                startState = start;
            else
                startState = Elements.FirstOrDefault();

            currentState = startState;
        }
    }
}
