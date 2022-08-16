using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using LeoEcsPhysics;
using Leopotam.Ecs;
using SkyCrush.WSGenerator;
using RiderGame.World;
using RiderGame.RuntimeData;

namespace RiderGame.Gameplay
{
    public class CoinCollectionSystem : IEcsRunSystem
    {
        private const float TimeToCollect = 0.2f;

        private readonly Generator _generator;
        private readonly SessionRuntimeData _sessionData;

        private readonly EcsFilter<OnTriggerEnter2DEvent> _fOnTriggerEnter;

        public void Run()
        {
            foreach (var i in _fOnTriggerEnter)
            {
                ref var eventData = ref _fOnTriggerEnter.Get1(i);

                if (!eventData.senderGameObject.FindActiveEntityWithComponent(out CoinCollectionArea area)) continue;

                if (!eventData.collider2D.gameObject.FindActiveEntityWithComponent<Coin>()) continue;

                OnCollectCoin(area.collider, eventData.collider2D);
            }
        }

        private void OnCollectCoin(Collider2D area, Collider2D coin)
        {
            coin.enabled = false;

            var sequence = DOTween.Sequence();
            sequence.Append(coin.transform.DOMove(area.transform.position + (Vector3)area.offset, TimeToCollect));

            sequence.OnComplete(() =>
            {
                _sessionData.CoinsCount.Value += 1;

                var container = _generator.PoolManager.GetPoolContainer(coin.name, true);
                container.Release(coin.gameObject);

                coin.enabled = true;
            });
        }
    }
}
