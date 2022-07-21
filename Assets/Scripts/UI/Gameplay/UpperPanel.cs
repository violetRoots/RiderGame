using UnityEngine;
using TMPro;
using RiderGame.RuntimeData;

namespace RiderGame.UI
{
    public class UpperPanel : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI coinsCountText;

        public void UpdatePanel(SessionRuntimeData runtimeData)
        {
            UpdateCoinsCount(runtimeData);
        }

        private void UpdateCoinsCount(SessionRuntimeData runtimeData)
        {
            coinsCountText.text = runtimeData.CoinsCount.ToString();
        }
    }
}
