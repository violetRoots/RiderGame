using RiderGame.RuntimeData;
using UnityEngine;

namespace RiderGame
{
    public class SessionStartup : MonoBehaviour
    {
        public GameplayRuntimeData GameplayRuntimeData { get; private set; }
        public SessionRuntimeData SessionRuntimeData { get; private set; }

        private void Awake()
        {
            GameplayRuntimeData = new GameplayRuntimeData();
            SessionRuntimeData = new SessionRuntimeData();
        }
    }
}
