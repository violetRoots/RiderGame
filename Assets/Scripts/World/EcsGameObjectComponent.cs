using System;
using UnityEngine;
using Voody.UniLeo;

namespace RiderGame.World
{
    public class EcsGameObjectComponent : MonoProvider<EcsGameObject> { }

    [Serializable]
    public struct EcsGameObject 
    {
        public GameObject instance;
    }
}
