using System;
using UnityEngine;
using Voody.UniLeo;
using RiderGame.Gameplay;
using UnityEngine.UI;

namespace RiderGame.UI
{
    public class WayPointIconComponent : MonoProvider<WayPointIcon>
    {
        public WayPointIcon Value { get => value; set => this.value = value; }
    }

    [Serializable]
    public struct WayPointIcon
    {
        public Image image;
    }
}
