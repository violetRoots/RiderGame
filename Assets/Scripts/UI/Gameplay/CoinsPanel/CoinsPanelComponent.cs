using System;
using UnityEngine;
using TMPro;
using Voody.UniLeo;

namespace RiderGame.UI
{
    public class CoinsPanelComponent : MonoProvider<CoinsPanel> { }

    [Serializable]
    public struct CoinsPanel
    {
        public TextMeshProUGUI coinsCountText;
    }
}
