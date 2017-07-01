using UnityEngine;
using UnityEngine.EventSystems;

namespace Metro.InputSystems
{
    public sealed class TapViewer : MonoBehaviour, IPointerDownHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("PointerDown");
        }
    }
}
