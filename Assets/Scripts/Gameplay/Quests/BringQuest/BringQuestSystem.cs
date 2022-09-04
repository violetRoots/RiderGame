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
        private const float QuestFailedDistanceOffset = 2.0f;

        private readonly GameplayRuntimeData _gameplayRuntimeData;

        private readonly EcsFilter<EcsGameObject, Player> _fPlayer;
        private readonly EcsFilter<EcsGameObject, StartBringQuest, ActivationEvent> _fQuestActivationEvent;
        private readonly EcsFilter<EcsGameObject, CompleteBringQuest, ActiveObject> _fCompleteQuest;
        private readonly EcsFilter<EcsGameObject, StartBringQuest, DeactivationEvent> _fQuestDeactivationEvent;
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

            foreach(var i in _fCompleteQuest)
            {
                ref var gameObject = ref _fCompleteQuest.Get1(i);
                ref var completeQuestComponent = ref _fCompleteQuest.Get2(i);

                if (!FindQuestByTarget(gameObject.instance, out IQuest quest)) continue;

                var failedPos = gameObject.instance.transform.position.y - completeQuestComponent.collider.bounds.size.y - QuestFailedDistanceOffset;
                if (failedPos >= _playerObject.transform.position.y)
                {
                    FailQuest(quest);
                }
            }

            foreach (var i in _fQuestDeactivationEvent)
            {
                ref var startQuestComponent = ref _fQuestDeactivationEvent.Get2(i);

                startQuestComponent.collider.enabled = true;
            }
        }

        private void StartQuest(GameObject startObject)
        {
            var quest = _quests.ToList().Find((q) => q is BringQuest bq && bq.StartObject != null && bq.StartObject == startObject);

            if (quest == null) return;

            quest.Status.Value = QuestStatus.InProgress;
        }

        private bool FindQuestByTarget(GameObject completeObject, out IQuest quest)
        {
            quest = _quests.ToList().Find((q) => q is BringQuest bq && bq.Target == completeObject.transform);

            return quest != null;
        }

        private void CompleteQuest(GameObject completeObject)
        {
            if (!FindQuestByTarget(completeObject, out IQuest quest)) return;

            quest.Status.Value = QuestStatus.Completed;
            _quests.Remove(quest);
        }

        private void FailQuest(IQuest quest)
        {
            quest.Status.Value = QuestStatus.Failed;
            _quests.Remove(quest);
        }

        private GameObject SpawnCompleteQuestObject(GameObject startQuestObject, BringQuestConfiguration questConfigs)
        {
            var pos = startQuestObject.transform.position;
            var angleOffset = Random.Range(-MaxAngleOffset, MaxAngleOffset);
            pos += Quaternion.Euler(0, 0, angleOffset) * Vector2.down * questConfigs.Distance;

            return ObjectActivationSystem.Instantiate(questConfigs.completeQuestPrefab, pos);
        }
    }
}
