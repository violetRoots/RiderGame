using UnityEngine;
using Leopotam.Ecs;
using System.Collections;

namespace RiderGame.World
{
    public class OneFrameEventSystem : IEcsInitSystem, IEcsRunSystem
    {
        private static EcsStartup EcsStartupObject;
        private readonly EcsStartup _ecsStartupObject;

        private readonly EcsFilter<OneFrameEvent> _fOneFrameEvent;

        public static void AddOneFrameEvent<T>(EcsEntity entity, T customEvent) where T : struct
        {
            EcsStartupObject.StartCoroutine(AddOneFrameEventProcess(entity, customEvent));
        }

        public void Init()
        {
            EcsStartupObject = _ecsStartupObject;
        }

        public void Run()
        {
            foreach (var i in _fOneFrameEvent)
            {
                ref var entity = ref _fOneFrameEvent.GetEntity(i);
                ref var oneFrameEvent = ref _fOneFrameEvent.Get1(i);

                oneFrameEvent.deleteAction?.Invoke();
                entity.Del<OneFrameEvent>();
            }
        }

        private static IEnumerator AddOneFrameEventProcess<T>(EcsEntity entity, T customEvent) where T : struct
        {
            yield return new WaitForEndOfFrame();

            entity.Replace(customEvent);

            OneFrameEvent oneFrameEvent = new OneFrameEvent()
            {
                deleteAction = () => entity.Del<T>()
            };
            entity.Replace(oneFrameEvent);
        }
    }
}
