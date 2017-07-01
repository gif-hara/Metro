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

        private Vector2 currentPosition;

        private float tapDuration;

        private CompositeDisposable pointerEvents = new CompositeDisposable();

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
            this.currentPosition = screenPosition;
            this.tapDuration = 0.0f;
            this.pointerEvents.Add(
                this.UpdateAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.tapDuration += Time.deltaTime;
                })
                .AddTo(this)
            );
            this.pointerEvents.Add(
                this.UpdateAsObservable()
                .Where(_ => this.CanPublishSwipe(this.currentPosition))
                .SubscribeWithState(this, (_, _this) =>
                {
                    UniRxEvent.GlobalBroker.Publish(Swipe.Get((this.beginPosition - this.currentPosition).normalized));
                })
                .AddTo(this)
            );
        }

        public void PointerUp(Vector2 screenPosition)
        {
            if (this.CanPublishTap(screenPosition))
            {
                UniRxEvent.GlobalBroker.Publish(Tap.Get(screenPosition));
            }
            this.pointerCanvasGroup.alpha = 0.0f;
            this.pointerEvents.Clear();
        }

        public void Drag(Vector2 screenPosition)
        {
            this.cachedPointerTransform.anchoredPosition = screenPosition;
            this.currentPosition = screenPosition;
        }

        private bool CanPublishTap(Vector2 lastPosition)
        {
            var tapDistance = (lastPosition - this.beginPosition).magnitude;
            tapDistance = tapDistance / this.region.rect.size.magnitude;
            return this.tapDuration < this.settings.TapDuration
                && tapDistance < this.settings.TapDistance;
        }

        public bool CanPublishSwipe(Vector2 screenPosition)
        {
            var distance = (screenPosition - this.beginPosition).magnitude;
            return distance > this.settings.SwipeDistance;
        }
    }
}
