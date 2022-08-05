using System;
using UnityEngine;
using Voody.UniLeo;

namespace RiderGame.UI
{
    public class UIElementComponent : MonoProvider<UIElement> { }

    [Serializable]
    public struct UIElement
    {
        public GameObject instance;
        public GameObject content;
    }
}
