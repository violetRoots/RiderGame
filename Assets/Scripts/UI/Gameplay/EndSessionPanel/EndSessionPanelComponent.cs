using System;
using UnityEngine;
using UnityEngine.UI;
using Voody.UniLeo;

namespace RiderGame.UI
{
    public class EndSessionPanelComponent : MonoProvider<EndSessionPanel> { }

    [Serializable]
    public struct EndSessionPanel
    {
        public Button restartButton;
    }
}
