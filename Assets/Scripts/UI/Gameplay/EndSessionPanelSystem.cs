using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using Leopotam.Ecs;
using RiderGame.RuntimeData;

namespace RiderGame.UI
{
    public class EndSessionPanelSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private readonly SessionRuntimeData _sessionData;

        private readonly EcsFilter<UIElement, EndSessionPanel> _fEndSessionPanel;

        public void Init()
        {
            InitButtons();

            _sessionData.Status.Subscribe(OnSessionStatusChanged);
        }

        public void Destroy()
        {
            _sessionData.Status.Dispose();
        }

        private void InitButtons()
        {
            foreach (var i in _fEndSessionPanel)
            {
                ref var endSessionPanel = ref _fEndSessionPanel.Get2(i);

                endSessionPanel.restartButton.onClick.AddListener(OnRestartButtonClicked);
            }
        }

        private void OnSessionStatusChanged(SessionStatus status)
        {
            var showPanel = status == SessionStatus.Ended;

            foreach (var i in _fEndSessionPanel)
            {
                ref var gameObject = ref _fEndSessionPanel.Get1(i);

                gameObject.content.SetActive(showPanel);
            }
        }

        private void OnRestartButtonClicked()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
