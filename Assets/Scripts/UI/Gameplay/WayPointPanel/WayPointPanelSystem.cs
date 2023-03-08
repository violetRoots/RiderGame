using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Leopotam.Ecs;
using DG.Tweening;
using RiderGame.RuntimeData;
using RiderGame.Gameplay;
using RiderGame.SO;
using RiderGame.World;

namespace RiderGame.UI
{
    public class WayPointPanelSystem : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
    {
        private readonly UIConfiguration _uiConfigs;
        private readonly GameplayRuntimeData _gameplayRuntimeData;

        private readonly EcsFilter<EcsGameObject, Player> _fPlayer;
        private readonly EcsFilter<UIElement, WayPointPanel> _fWayPointPanel;
        private readonly EcsFilter<UIElement, WayPointIcon> _fWayPointIcon;

        private readonly Dictionary<GameObject, BringQuest> _wayPointQuests = new Dictionary<GameObject, BringQuest>();
        private NotifyCollectionChangedEventHandler _questCollectionHandler;
        private Camera _camera;
        private Transform _playerTransform;

        public void Init()
        {
            _questCollectionHandler = new NotifyCollectionChangedEventHandler(OnQuestCollectionChanged);
            _gameplayRuntimeData.Quests.CollectionChanged += _questCollectionHandler;

            _playerTransform = _fPlayer.Get1(0).instance.transform;
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

                float minY = pointIcon.image.GetPixelAdjustedRect().height / 1.5f;
                float maxY = Screen.height - minY;

                var position = _camera.WorldToScreenPoint(quest.Target.position + new Vector3(0, _uiConfigs.YWayPointOffset, 0));
                position.x = Mathf.Clamp(position.x, minX, maxX);
                position.y = Mathf.Clamp(position.y, minY, maxY);

                uiElement.instance.transform.position = position;
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

                var wayPoint = GameObject.Instantiate(_uiConfigs.WayPointIcon, uiElement.content.transform);
                var wayPointImage = wayPoint.GetComponent<Image>();

                wayPoint.transform.localScale = Vector3.zero;
                wayPointImage.sprite = quest.Configs.wayPointIcon;

                quest.Status.Subscribe((newStatus) => OnQuestStatusChanged(wayPoint, quest, newStatus));

                _wayPointQuests.Add(wayPoint, quest);
            }
        }

        private void OnQuestStatusChanged(GameObject wayPoint, BringQuest quest, QuestStatus newStatus)
        {
            if (newStatus == QuestStatus.InProgress)
            {
                ShowWayPoint(wayPoint, quest);
            }
            else if (newStatus == QuestStatus.Completed || newStatus == QuestStatus.Failed)
            {
                quest.Status.Dispose();

                SetWayPointIconVisible(wayPoint.transform, false, () => GameObject.Destroy(wayPoint));
            }
        }

        private void ShowWayPoint(GameObject wayPoint, BringQuest quest)
        {
            foreach (var i in _fWayPointPanel)
            {
                ref var uiElement = ref _fWayPointPanel.Get1(i);

                var questIconTransform = GameObject.Instantiate(_uiConfigs.QuestIcon, uiElement.content.transform).transform;

                questIconTransform.position = _camera.WorldToScreenPoint(_playerTransform.position);
                var questIconImage = questIconTransform.GetComponent<Image>();
                questIconImage.sprite = quest.Configs.questIcon;

                ShowQuestIconAbovePlayer(questIconTransform, questIconImage, _playerTransform, 
                    () => MoveIconToWayPoint(questIconTransform, questIconImage, wayPoint.transform));
            }
        }

        private void ShowQuestIconAbovePlayer(Transform icon, Image iconImage, Transform player, Action onComplete)
        {
            var yStartOffset = 5.0f;
            var yEndOffset = 7.0f;
            var timeToOffset = 0.5f;

            iconImage.color = new Color(1, 1, 1, 0);

            var startPos = _camera.WorldToScreenPoint(player.position + new Vector3(0.0f, yStartOffset));
            var endPos = _camera.WorldToScreenPoint(player.position + new Vector3(0.0f, yEndOffset));

            var sequence = DOTween.Sequence();
            sequence.Append(icon.DOMove(endPos, timeToOffset).SetEase(Ease.Linear));
            sequence.Join(iconImage.DOFade(1.0f, timeToOffset));
            sequence.OnComplete(() => onComplete?.Invoke());
        }

        private void MoveIconToWayPoint(Transform icon, Image questIcon, Transform wayPoint)
        {
            var timeToMoveToWayPoint = 1.0f;
            var endScale = new Vector3(0.5f, 0.5f, 1.0f);

            var sequence = DOTween.Sequence();
            sequence.Append(icon.DOMove(wayPoint.position, timeToMoveToWayPoint).SetEase(Ease.Linear));
            sequence.Join(icon.DOScale(endScale, timeToMoveToWayPoint).SetEase(Ease.Linear));
            //sequence.Join(questIcon.DOFade(0.0f, timeToMoveToWayPoint).SetEase(Ease.InBack));
            sequence.OnComplete(() =>
            {
                SetWayPointIconVisible(wayPoint, true);

                GameObject.Destroy(icon.gameObject);
            });
        }

        private static void SetWayPointIconVisible(Transform wayPoint, bool value, Action onComplete = null)
        {
            var targetScale = value ? Vector3.one : Vector3.zero;
            var timeToScale = 0.1f;

            wayPoint.DOScale(targetScale, timeToScale).OnComplete(() => onComplete?.Invoke());
        }
    }
}
