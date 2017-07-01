using UnityEngine;

namespace Metro.InputSystems
{
    [CreateAssetMenu(menuName = "Metro/Settings/Input")]
    public sealed class InputSettings : ScriptableObject
    {
        [SerializeField]
        private float tapDuration;

        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float tapDistance;

        [SerializeField]
        private float swipeDistance;

        /// <summary>
        /// タップを許容する入力時間
        /// </summary>
        /// <remarks>
        /// タッチを開始した時間と終了した時間がこの値より小さい場合はタップしたとみなします
        /// </remarks>
        public float TapDuration
        {
            get { return tapDuration; }
            private set { this.tapDuration = Mathf.Max(0.1f, value); }
        }

        /// <summary>
        /// タップを許容する距離
        /// </summary>
        /// <remarks>
        /// 最初にタッチした座標と最後の座標との距離を領域のサイズで正規化した値がこの値より小さい場合はタップとみなします
        /// </remarks>
        public float TapDistance
        {
            get { return this.tapDistance; } 
            private set { this.tapDistance = Mathf.Clamp01(value); }
        }

        /// <summary>
        /// スワイプだと判断するタッチ移動距離
        /// </summary>
        public float SwipeDistance
        {
            get { return this.swipeDistance; } 
            private set { this.swipeDistance = Mathf.Clamp01(value); }
        }

        void OnValidate()
        {
            this.TapDuration = this.tapDuration;
            this.TapDistance = this.tapDistance;
            this.SwipeDistance = this.swipeDistance;
        }
    }
}
