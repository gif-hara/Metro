using System;
using HK.Framework.EventSystems;
using Metro.Events.InputSystems;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Metro.InputSystems
{
    public sealed class InputSystemViewer : MonoBehaviour
    {
        [SerializeField]
        private InputSettings settings;
        
        [SerializeField]
        private RectTransform region;
        
        [SerializeField]
        private CanvasGroup pointerCanvasGroup;

        private Vector2 cachedRegionSize;

        private RectTransform cachedPointerTransform;

        private Vector2 beginPosition;

        private float tapDuration;

        private IDisposable tapDurationTimer;

        void Awake()
        {
            Assert.IsNotNull(this.region);
            Assert.IsNotNull(this.pointerCanvasGroup);
            {
                var anchorSize = this.region.anchorMax - this.region.anchorMin;
                this.cachedRegionSize.Set(anchorSize.x * Screen.width, anchorSize.y * Screen.height);
            }
            {
                this.cachedPointerTransform = this.pointerCanvasGroup.transform as RectTransform;
                Assert.IsNotNull(this.cachedPointerTransform);
            }
            {
                this.pointerCanvasGroup.alpha = 0.0f;
            }
        }

        public void PointerDown(Vector2 screenPosition)
        {
            this.pointerCanvasGroup.alpha = 1.0f;
            this.cachedPointerTransform.anchoredPosition = screenPosition;
            this.beginPosition = screenPosition;
            this.tapDuration = 0.0f;
            this.tapDurationTimer = this.UpdateAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.tapDuration += Time.deltaTime;
                })
                .AddTo(this);
        }

        public void PointerUp(Vector2 screenPosition)
        {
            if (this.tapDuration < this.settings.TapDuration)
            {
                UniRxEvent.GlobalBroker.Publish(Tap.Get(screenPosition));
            }
            this.pointerCanvasGroup.alpha = 0.0f;
            this.tapDurationTimer.Dispose();
        }

        public void Drag(Vector2 screenPosition)
        {
            this.cachedPointerTransform.anchoredPosition = screenPosition;
        }
    }
}
