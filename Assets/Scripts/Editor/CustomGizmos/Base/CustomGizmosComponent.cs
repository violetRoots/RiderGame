using System;
using Voody.UniLeo;

namespace RiderGame.Editor.CustomGizmos
{
    public class CustomGizmosComponent : MonoProvider<CustomGizmos> { }

    [Serializable]
    public struct CustomGizmos
    {
        public BaseGizmosDrawer drawer;
    }
}