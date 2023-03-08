using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace RiderGame.UI
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class LifeIconController : MonoBehaviour
    {
        public ReactiveProperty<bool> IsActiveIcon { get; private set; } = new ReactiveProperty<bool>();

        [SerializeField] private Image image;

        [Space(10)]
        [SerializeField] private Sprite activeIcon;
        [SerializeField] private Sprite inactiveIcon;

        private IDisposable _activeStateChanged;

        private void OnEnable()
        {
            _activeStateChanged = IsActiveIcon.Subscribe(OnIsActiveIconValueChanged);
        }

        private void OnDisable()
        {
            _activeStateChanged?.Dispose();
            _activeStateChanged = null;
        }

        public void OnIsActiveIconValueChanged(bool isActive)
        {
            image.sprite = isActive ? activeIcon : inactiveIcon;
        }
    }
}
