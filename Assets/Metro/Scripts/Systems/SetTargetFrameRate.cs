using UnityEngine;

namespace Metro.Systems
{
    public sealed class SetTargetFrameRate : MonoBehaviour
    {
        [SerializeField]
        private int targetFrameRate;
        
        void Awake()
        {
            Application.targetFrameRate = targetFrameRate;
        }
    }
}
