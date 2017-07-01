using UnityEngine;

namespace Metro.InputSystems
{
    [CreateAssetMenu(menuName = "Metro/Settings/Input")]
    public sealed class InputSettings : ScriptableObject
    {
        [SerializeField]
        private float tapDuration;

        [SerializeField]
        private float tapDistance;

        public float TapDuration
        {
            get { return tapDuration; }
            private set { this.tapDuration = Mathf.Max(0.1f, value); }
        }

        public float TapDistance
        {
            get { return this.tapDistance; } 
            private set { this.tapDistance = Mathf.Max(0.1f, value); }
        }

        void OnValidate()
        {
            this.TapDuration = this.tapDuration;
            this.TapDistance = this.tapDistance;
        }
    }
}
