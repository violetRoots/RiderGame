using System;
using UnityEngine;
using TMPro;
using Voody.UniLeo;

namespace RiderGame.UI
{
    public class UpperPanelComponent : MonoProvider<UpperPanel> { }

    [Serializable]
    public struct UpperPanel
    {
        public TextMeshProUGUI coinsCountText;
    }
}
