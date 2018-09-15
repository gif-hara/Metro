using HK.Framework.EventSystems;
using HK.Framework.Extensions;
using UnityEngine;

namespace Metro.Events.InputSystems
{
    /// <summary>
    /// タップ時のイベント
    /// </summary>
    public sealed class Tap : Message<Tap, Vector2>
    {
        public Vector2 ScreenPosition { get { return this.param1; } }
    }

    /// <summary>
    /// スワイプ時のイベント
    /// </summary>
    public sealed class Swipe : Message<Swipe, Vector2>
    {
        public Vector2 Normalize { get { return this.param1; } }
    }

    /// <summary>
    /// フリック時のイベント
    /// </summary>
    public sealed class Flick : Message<Flick, Vector2>
    {
        public Vector2 Normalize { get { return this.param1; } }
        
        /// <summary>
        /// 入力方向の角度を返す
        /// </summary>
        public float Angle { get { return Mathf.Atan2(this.Normalize.y, this.Normalize.x) * Mathf.Rad2Deg; } }
    }

    /// <summary>
    /// タッチされた際のイベント
    /// </summary>
    public sealed class PointerDown : Message<PointerDown, Vector2>
    {
        public Vector2 ScreenPosition { get { return this.param1; } }
    }
    
    /// <summary>
    /// 指が離れた際のイベント
    /// </summary>
    public sealed class PointerUp : Message<PointerUp, Vector2>
    {
        public Vector2 ScreenPosition { get { return this.param1; } }
    }
}
