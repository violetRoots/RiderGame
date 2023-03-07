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
        public State StartState => startState;
        public bool IsInited => _isInited;
        public ReactiveProperty<State> ActiveState { get; private set; }

        [AllowNesting]
        [ReadOnly]
        [SerializeField]
        private State startState;

        private bool _isInited;

        public StateRuntimeController(List<StateContainer> containers) : base(containers)
        {
            startState = Elements.FirstOrDefault();
        }

        public bool TryGetActiveStateAs<T>(out T state) where T : State
        {
            if (!_isInited)
            {
                state = default;
                return false;
            }

            state = ActiveState.Value as T;
            return ActiveState.Value is T;
        }

        public bool TrySetActiveStateAs<T>() where T : State
        {
            if (!_isInited) return false;

            bool hasState = TryGet(out T state);

            if (hasState)
            {
                state.DirectionAngle = ActiveState.Value.DirectionAngle;
                ActiveState.Value = state;
            }

            return hasState;
        }

        public void Init()
        {
            ActiveState = new ReactiveProperty<State>(StartState);

            _isInited = true;
        }
    }
}
