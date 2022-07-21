using UnityEngine;

namespace RiderGame.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private SessionStartup sessionStartup;

        [Space(10)]
        [SerializeField]
        private UpperPanel upperPanel;

        private void Update()
        {
            upperPanel.UpdatePanel(sessionStartup.SessionRuntimeData);
        }
    }
}
