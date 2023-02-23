using System;
using UnityEngine;
using RiderGame.SO;

namespace RiderGame.Gameplay
{
    [Serializable]
    public abstract class Modifier : ContainerElement
    {
        public bool IsActive => _isActive;

        [SerializeField]
        private bool _isActive;
        

        public void Activate()
        {
            _isActive = true;
        }

        public void Deactivate()
        {
            _isActive = false;
        }
    }
}
