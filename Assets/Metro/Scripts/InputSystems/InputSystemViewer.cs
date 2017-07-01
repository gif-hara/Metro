using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace Metro.InputSystems
{
    public sealed class InputSystemViewer : MonoBehaviour
    {
        [SerializeField]
        private RectTransform region;
        
        [SerializeField]
        private RectTransform pointer;

        private Vector2 cachedRegionSize;

        void Awake()
        {
            var anchorSize = this.region.anchorMax - this.region.anchorMin;
            this.cachedRegionSize.Set(anchorSize.x * Screen.width, anchorSize.y * Screen.height);
            Debug.Log(this.cachedRegionSize);
            this.UpdateAsObservable()
                .Where(_ => this.isActiveAndEnabled)
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.Begin(Input.mousePosition);
                });
        }

        void Start()
        {
        }

        public void Begin(Vector2 screenPosition)
        {
            this.pointer.anchoredPosition = screenPosition;
        }
        
    }
}
