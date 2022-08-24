using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using Leopotam.Ecs;
using LeoEcsPhysics;
using RiderGame.World;
using RiderGame.SO;
using RiderGame.RuntimeData;


namespace RiderGame.Gameplay
{
    public class BringQuestSystem : IEcsInitSystem, IEcsRunSystem
    {
        private const float MaxAngleOffset = 15.0f;

        private readonly GameplayRuntimeData _gameplayRuntimeData;

        private readonly EcsFilter<EcsGameObject, Player> _fPlayer;
        private readonly EcsFilter<EcsGameObject, StartBringQuest, ActivationEvent> _fQuestActivationEvent;
        private readonly EcsFilter<OnTriggerEnter2DEvent> _fTriggerEnter;

        private ObservableCollection<IQuest> _quests;
        private GameObject _playerObject;

        public void Init()
        {
            _quests = _gameplayRuntimeData.Quests;

            _playerObject = _fPlayer.Get1(0).instance;
        }
        public void Run()
        {
            foreach(var i in _fQuestActivationEvent)
            {
                ref var gameObject = ref _fQuestActivationEvent.Get1(i);
                var questConfigs = _fQuestActivationEvent.Get2(i).questConfigs;

                var completetQuestObject = SpawnCompleteQuestObject(gameObject.instance, questConfigs);

                BringQuest quest = new BringQuest(questConfigs, gameObject.instance, completetQuestObject.transform);
                _quests.Add(quest);
            }

            foreach(var i in _fTriggerEnter)
            {
                ref var eventData = ref _fTriggerEnter.Get1(i);

                var senderObject = eventData.senderGameObject;
                var collider = eventData.collider2D;

                if (!senderObject.FindActiveEntityWithComponent<Player>()) continue;

                if (collider.gameObject.FindActiveEntityWithComponent<StartBringQuest>())
                {
                    collider.enabled = false;

                    StartQuest(collider.gameObject);
                }
                else if (collider.gameObject.FindActiveEntityWithComponent<CompleteBringQuest>())
                {
                    CompleteQuest(collider.gameObject);
                }
            }
        }

        private void StartQuest(GameObject startObject)
        {
            var quest = _quests.ToList().Find((q) => q is BringQuest bq && bq.StartObject != null && bq.StartObject == startObject);

            if (quest == null) return;

            Debug.Log("Start Quest");
            quest.Status.Value = QuestStatus.InProgress;
        }

        private void CompleteQuest(GameObject completeObject)
        {
            var quest = _quests.ToList().Find((q) => q is BringQuest bq && bq.Target == completeObject.transform);

            if (quest == null) return;

            Debug.Log("Complete Quest");
            quest.Status.Value = QuestStatus.Completed;
            _quests.Remove(quest);
        }

        private GameObject SpawnCompleteQuestObject(GameObject startQuestObject, BringQuestConfiguration questConfigs)
        {
            var pos = startQuestObject.transform.position;
            var angleOffset = Random.Range(-MaxAngleOffset, MaxAngleOffset);
            pos += Quaternion.Euler(0, 0, angleOffset) * Vector2.down * questConfigs.Distance;

            return ObjectActivationSystem.Instantiate(questConfigs.CompleteQuestPrefab, pos);
        }
    }
}
