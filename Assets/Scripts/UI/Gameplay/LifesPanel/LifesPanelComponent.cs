using System;
using UnityEngine;
using Voody.UniLeo;

namespace RiderGame.UI
{
    public class LifesPanelComponent : MonoProvider<LifesPanel> { }

    [Serializable]
    public struct LifesPanel
    {
        public LifeIconController iconPrefab;

        [HideInInspector]
        public LifeIconController[] icons;

        [HideInInspector]
        public IDisposable updateSubscription;
    }
}
