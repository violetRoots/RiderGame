using UnityEngine;
using Leopotam.Ecs;
using UniRx;

namespace RiderGame.RuntimeData
{
    public class UpdateSessionDataSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private SessionRuntimeData _sessionData;
        public void Init()
        {
            _sessionData.Status.Value = SessionStatus.Playing;

            _sessionData.Status.Subscribe(Observer.Create<SessionStatus>(OnStatusChanged));
        }

        public void Destroy()
        {
            _sessionData.Status.Dispose();
        }

        private void OnStatusChanged(SessionStatus status)
        {
            switch (status)
            {
                case SessionStatus.NotStarted:
                    PauseSession();
                    break;
                case SessionStatus.Playing:
                    PlaySession();
                    break;
                case SessionStatus.Paused:
                    PauseSession();
                    break;
                case SessionStatus.Ended:
                    PauseSession();
                    break;
                default:
                    throw new System.NotImplementedException();
            }
        }

        private void PlaySession()
        {
            Time.timeScale = 1.0f;
        }

        private void PauseSession()
        {
            Time.timeScale = 0.0f;
        }
    }
}
