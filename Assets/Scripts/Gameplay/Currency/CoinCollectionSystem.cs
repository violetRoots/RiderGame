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

        private readonly EcsFilter<EcsGameObject, ActiveObject, Coin> _fCoin;
        private readonly EcsFilter<CoinCollectionArea> _fCollectionArea;
        private readonly EcsFilter<OnTriggerEnter2DEvent> _fOnTriggerEnter;

        private readonly List<GameObject> _coins = new List<GameObject>();
        private readonly List<Collider2D> _coinAreas = new List<Collider2D>();

        public void Run()
        {
            _coins.Clear();
            foreach(var i in _fCoin)
            {
                ref var gameObject = ref _fCoin.Get1(i);
                _coins.Add(gameObject.instance);
            }

            _coinAreas.Clear();
            foreach (var i in _fCollectionArea)
            {
                ref var area = ref _fCollectionArea.Get1(i);
                _coinAreas.Add(area.collider);
            }

            foreach (var i in _fOnTriggerEnter)
            {
                ref var eventData = ref _fOnTriggerEnter.Get1(i);

                foreach(var area in _coinAreas)
                {
                    if (area.gameObject != eventData.senderGameObject) continue;

                    foreach(var coin in _coins)
                    {
                        if (coin != eventData.collider2D.gameObject) continue;

                        OnCollectCoin(area, eventData.collider2D);
                    }
                }
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
