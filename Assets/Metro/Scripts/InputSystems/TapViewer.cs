using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Metro.InputSystems
{
    public sealed class TapViewer : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField]
        private InputSystemViewer viewer;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            this.viewer.PointerDown(eventData.position);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            this.viewer.PointerUp();
        }
    }
}
