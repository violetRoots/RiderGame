using System;
using UnityEngine;

namespace RiderGame.Gameplay
{
    [Serializable]
    public abstract class Modifier : ScriptableObject
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
