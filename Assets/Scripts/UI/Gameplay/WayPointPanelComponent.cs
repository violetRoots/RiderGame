using System;
using UnityEngine;
using UnityEngine.UI;
using Voody.UniLeo;

namespace RiderGame.UI
{
    public class WayPointPanelComponent : MonoProvider<WayPointPanel> { }

    [Serializable]
    public struct WayPointPanel
    {
        public GameObject wayPointIcon;
    }
}
