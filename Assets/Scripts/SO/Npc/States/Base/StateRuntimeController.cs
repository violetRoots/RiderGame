using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using NaughtyAttributes;
using RiderGame.Gameplay;
using UniRx;

namespace RiderGame.SO
{
    [Serializable]
    public class StateRuntimeController : ContainerElementsController<StateContainer, State>
    {
        public ReactiveProperty<State> ActiveState { get; private set; } = new ReactiveProperty<State>();

        [AllowNesting]
        [ReadOnly]
        [SerializeField]
        private State startState;

        [AllowNesting]
        [ReadOnly]
        [SerializeField]
        private State showActiveState;

        private IDisposable _updateShowActiveStateSubscription;

        public StateRuntimeController(List<StateContainer> containers, State start) : base(containers)
        {
            if (start != null)
                startState = start;
            else
                startState = Elements.FirstOrDefault();

            _updateShowActiveStateSubscription = ActiveState.Subscribe(UpdateShowActiveStateValue);
            ActiveState.Value = startState;
        }

        ~StateRuntimeController()
        {
            _updateShowActiveStateSubscription?.Dispose();
            _updateShowActiveStateSubscription = null;
        }

        public bool TryGetActiveStateAs<T>(out T state) where T : State
        {
            state = ActiveState.Value as T;
            return ActiveState.Value is T;
        }

        public bool TrySetActiveStateAs<T>() where T : State
        {
            bool hasState = TryGet(out T state);

            if (hasState)
                ActiveState.Value = state;

            return hasState;
        }

        private void UpdateShowActiveStateValue(State state)
        {
            showActiveState = state;
        }
    }
}
