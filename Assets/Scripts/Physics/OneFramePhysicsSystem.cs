using LeoEcsPhysics;
using Leopotam.Ecs;
using System.Collections;
using UnityEngine;

namespace RiderGame.Physics
{
    public class OneFramePhysicsSystem : IEcsRunSystem
    {
        private readonly EcsStartup _ecsStartupObject;

        private readonly EcsFilter<OnCollisionEnter2DEvent> _fCollisionEnter;
        private readonly EcsFilter<OnCollisionStay2DEvent> _fCollisionStay;
        private readonly EcsFilter<OnCollisionExit2DEvent> _fCollisionExit;

        private readonly EcsFilter<OnTriggerEnter2DEvent> _fTriggerEnter;
        private readonly EcsFilter<OnTriggerStay2DEvent> _fTriggerStay;
        private readonly EcsFilter<OnTriggerExit2DEvent> _fTriggerExit;

        public void Run()
        {
            _ecsStartupObject.StartCoroutine(OneFrameProcess());
        }

        private IEnumerator OneFrameProcess()
        {
            yield return null;

            foreach (var i in _fCollisionEnter)
                _fCollisionEnter.GetEntity(i).Del<OnCollisionEnter2DEvent>();

            foreach (var i in _fCollisionStay)
                _fCollisionStay.GetEntity(i).Del<OnCollisionStay2DEvent>();

            foreach (var i in _fCollisionExit)
                _fCollisionExit.GetEntity(i).Del<OnCollisionExit2DEvent>();

            foreach (var i in _fTriggerEnter)
                _fTriggerEnter.GetEntity(i).Del<OnTriggerEnter2DEvent>();

            foreach (var i in _fTriggerStay)
                _fTriggerStay.GetEntity(i).Del<OnTriggerStay2DEvent>();

            foreach (var i in _fTriggerExit)
                _fTriggerExit.GetEntity(i).Del<OnTriggerExit2DEvent>();
        }
    }
}
