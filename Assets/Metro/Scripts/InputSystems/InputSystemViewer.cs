using System;
using System.Security.Cryptography;
using System.Text;
using HK.Framework.EventSystems;
using Metro.Events.InputSystems;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

namespace Metro.InputSystems
{
    public sealed class InputSystemViewer : MonoBehaviour
    {
        [SerializeField]
        private InputSettings settings;
        
        [SerializeField]
        private RectTransform region;
        
        [SerializeField]
        private CanvasGroupData currentPointer;

        [SerializeField]
        private CanvasGroupData tapPointer;

        [SerializeField]
        private CanvasGroupData delayPointer;

        private Vector2 cachedRegionSize;

        private Vector2 beginPosition;

        private Vector2 currentPosition;

        private float tapDuration;

        private CompositeDisposable pointerEvents = new CompositeDisposable();

        void Awake()
        {
            Assert.IsNotNull(this.region);
            Assert.IsNotNull(this.currentPointer);
            {
                var anchorSize = this.region.anchorMax - this.region.anchorMin;
                this.cachedRegionSize.Set(anchorSize.x * Screen.width, anchorSize.y * Screen.height);
            }
            {
                this.SetAlpha(0.0f);
            }
        }

        public void PointerDown(Vector2 screenPosition)
        {
            this.SetAlpha(1.0f);
            
            this.currentPointer.CachedTransform.anchoredPosition = screenPosition;
            this.tapPointer.CachedTransform.anchoredPosition = screenPosition;
            this.delayPointer.CachedTransform.anchoredPosition = screenPosition;
            this.beginPosition = screenPosition;
            this.currentPosition = screenPosition;
            this.tapDuration = 0.0f;

            var updateStream = this.UpdateAsObservable();
            
            // タップ秒数を更新する
            this.pointerEvents.Add(
                updateStream
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.tapDuration += Time.deltaTime;
                })
                .AddTo(this)
            );
            
            // スワイプイベントを発行する
            this.pointerEvents.Add(
                updateStream
                .First(_ => this.CanPublishSwipe(this.currentPosition))
                .SubscribeWithState(this, (_, _this) =>
                    {
                        _this.pointerEvents.Add(_this.PublishSwipe(updateStream));
                    })
                .AddTo(this)
            );
            
            this.pointerEvents.Add(
                updateStream
                .Select(_ => this.currentPosition)
                .Buffer(this.settings.FlickBuffer, 1)
                .Subscribe(p =>
                    {
                        this.delayPointer.CachedTransform.anchoredPosition = p[0];
                    })
                );
            
            UniRxEvent.GlobalBroker.Publish(Events.InputSystems.PointerDown.Get(screenPosition));
        }

        public void PointerUp(Vector2 screenPosition)
        {
            if (this.CanPublishTap(screenPosition))
            {
                UniRxEvent.GlobalBroker.Publish(Tap.Get(screenPosition));
            }
            this.SetAlpha(0.0f);
            UniRxEvent.GlobalBroker.Publish(Events.InputSystems.PointerUp.Get(screenPosition));

            this.pointerEvents.Clear();
        }

        public void Drag(Vector2 screenPosition)
        {
            this.currentPointer.CachedTransform.anchoredPosition = screenPosition;
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
            distance = distance / this.region.rect.size.magnitude;
            return distance > this.settings.SwipeDistance;
        }

        private void SetAlpha(float value)
        {
            this.currentPointer.CanvasGroup.alpha = value;
            this.tapPointer.CanvasGroup.alpha = value;
            this.delayPointer.CanvasGroup.alpha = value;
        }

        private IDisposable PublishSwipe(UniRx.IObservable<Unit> updateStream)
        {
            return updateStream
                .SubscribeWithState(this, (_, _this) =>
                {
                    UniRxEvent.GlobalBroker.Publish(Swipe.Get((_this.beginPosition - _this.currentPosition).normalized));
                })
                .AddTo(this);
        }

        [Serializable]
        private class CanvasGroupData
        {
            [SerializeField]
            private CanvasGroup canvasGroup;

            private RectTransform cachedTransform;

            public CanvasGroup CanvasGroup
            {
                get { return this.canvasGroup; }
            }

            public RectTransform CachedTransform
            {
                get
                {
                    if (this.cachedTransform == null)
                    {
                        this.cachedTransform = this.canvasGroup.transform as RectTransform;
                        Assert.IsNotNull(this.cachedTransform);
                    }
                    return cachedTransform;
                }
            }
        }
    }
}
