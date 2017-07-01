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
        private RectTransform region;
        
        [SerializeField]
        private CanvasGroup pointerCanvasGroup;

        private Vector2 cachedRegionSize;

        private RectTransform cachedPointerTransform;

        private Vector2 beginPosition;

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
        }

        public void PointerUp()
        {
            this.pointerCanvasGroup.alpha = 0.0f;
        }

        public void Drag(Vector2 screenPosition)
        {
            this.cachedPointerTransform.anchoredPosition = screenPosition;
        }
    }
}
