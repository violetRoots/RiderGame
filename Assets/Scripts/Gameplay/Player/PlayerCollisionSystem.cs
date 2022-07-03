using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;
using LeoEcsPhysics;
using RiderGame.World;

namespace RiderGame.Gameplay
{
    public class PlayerCollisionSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsFilter<EcsGameObject, Player> _fPlayer;
        private readonly EcsFilter<EcsGameObject, ActiveObject, Obstacle> _fObstacle;
        private readonly EcsFilter<OnCollisionEnter2DEvent> _fCollisionEnter;

        private GameObject _player;
        private readonly List<GameObject> _obstacles = new List<GameObject>();

        public void Init()
        {
            _player = _fPlayer.Get1(0).instance;
        }

        public void Run()
        {
            _obstacles.Clear();
            foreach (var i in _fObstacle)
            {
                ref var gameObject = ref _fObstacle.Get1(i);

                _obstacles.Add(gameObject.instance);
            }

            foreach (var i in _fCollisionEnter)
            {
                ref var eventData = ref _fCollisionEnter.Get1(i);

                if (eventData.senderGameObject != _player) return;

                var collisionObject = eventData.collider2D.gameObject;

                if (_obstacles.Contains(collisionObject))
                {
                    Debug.Log("Death");
                }
            }
        }
    }
}
