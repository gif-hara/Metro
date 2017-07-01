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
        private CanvasGroup pointer;

        private Vector2 cachedRegionSize;

        private RectTransform cachedPointerTransform;

        void Awake()
        {
            Assert.IsNotNull(this.region);
            Assert.IsNotNull(this.pointer);
            {
                var anchorSize = this.region.anchorMax - this.region.anchorMin;
                this.cachedRegionSize.Set(anchorSize.x * Screen.width, anchorSize.y * Screen.height);
            }
            {
                this.cachedPointerTransform = this.pointer.transform as RectTransform;
                Assert.IsNotNull(this.cachedPointerTransform);
            }
        }

        void Start()
        {
        }

        public void PointerDown(Vector2 screenPosition)
        {
            this.pointer.alpha = 1.0f;
            this.cachedPointerTransform.anchoredPosition = screenPosition;
        }

        public void PointerUp()
        {
            this.pointer.alpha = 0.0f;
        }
        
    }
}
