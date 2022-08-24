using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UniRx;
using Leopotam.Ecs;
using RiderGame.RuntimeData;
using RiderGame.Gameplay;
using System;

namespace RiderGame.UI
{
    public class WayPointPanelSystem : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
    {
        private const float YWayPointOffset = 2.5f;

        private readonly GameplayRuntimeData _gameplayRuntimeData;

        private readonly EcsFilter<UIElement, WayPointPanel> _fWayPointPanel;
        private readonly EcsFilter<UIElement, WayPointIcon> _fWayPointIcon;

        private readonly Dictionary<GameObject, BringQuest> _wayPointQuests = new Dictionary<GameObject, BringQuest>();
        private NotifyCollectionChangedEventHandler _questCollectionHandler;
        private Camera _camera;
        private Vector2 _position;

        public void Init()
        {
            _questCollectionHandler = new NotifyCollectionChangedEventHandler(OnQuestCollectionChanged);
            _gameplayRuntimeData.Quests.CollectionChanged += _questCollectionHandler;

            _camera = Camera.main;
        }

        public void Run()
        {
            foreach(var i in _fWayPointIcon)
            {
                ref var uiElement = ref _fWayPointIcon.Get1(i);
                ref var pointIcon = ref _fWayPointIcon.Get2(i);

                if(!_wayPointQuests.TryGetValue(uiElement.instance, out BringQuest quest)) continue;

                if (quest.Status.Value != QuestStatus.InProgress) continue;

                float minX = pointIcon.image.GetPixelAdjustedRect().width / 2;
                float maxX = Screen.width - minX;

                float minY = pointIcon.image.GetPixelAdjustedRect().height / 2;
                float maxY = Screen.height - minY;

                _position = _camera.WorldToScreenPoint(quest.Target.position + new Vector3(0, YWayPointOffset, 0));
                _position.x = Mathf.Clamp(_position.x, minX, maxX);
                _position.y = Mathf.Clamp(_position.y, minY, maxY);

                uiElement.instance.transform.position = _position;
            }
        }

        public void Destroy()
        {
            _gameplayRuntimeData.Quests.CollectionChanged -= _questCollectionHandler;
        }

        private void OnQuestCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var newQuest in e.NewItems) OnNewQuestInit((IQuest)newQuest);
            }
        }

        private void OnNewQuestInit(IQuest newQuest)
        {
            if (newQuest is not BringQuest newBringQuest) return;

            CreateNewWayPoint(newBringQuest);
        }

        private void CreateNewWayPoint(BringQuest quest)
        {
            foreach (var i in _fWayPointPanel)
            {
                ref var uiElement = ref _fWayPointPanel.Get1(i);
                ref var wayPointPanel = ref _fWayPointPanel.Get2(i);

                var wayPointIcon = GameObject.Instantiate(wayPointPanel.wayPointIcon, uiElement.content.transform);

                quest.Status.Subscribe((newStatus) => OnQuestStatusChanged(quest.Status, wayPointIcon, newStatus));

                _wayPointQuests.Add(wayPointIcon.gameObject, quest);
            }
        }

        private void OnQuestStatusChanged(IDisposable property, GameObject wayPoint, QuestStatus newStatus)
        {
            Debug.Log(newStatus);
            if (newStatus == QuestStatus.InProgress)
            {
                wayPoint.SetActive(true);
            }
            else if (newStatus == QuestStatus.Completed || newStatus == QuestStatus.Failed)
            {
                property.Dispose();
                GameObject.Destroy(wayPoint);
            }
        }
    }
}
