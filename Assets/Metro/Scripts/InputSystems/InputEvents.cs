using HK.Framework.EventSystems;
using UnityEngine;

namespace Metro.Events.InputSystems
{
    /// <summary>
    /// タップ時のイベント
    /// </summary>
    public sealed class Tap : UniRxEvent<Tap, Vector2>
    {
        public Vector2 ScreenPosition { get { return this.param1; } }
    }

    /// <summary>
    /// スワイプ時のイベント
    /// </summary>
    public sealed class Swipe : UniRxEvent<Swipe, Vector2>
    {
        public Vector2 Direction { get { return this.param1; } }
    }

    /// <summary>
    /// フリック時のイベント
    /// </summary>
    public sealed class Flick : UniRxEvent<Flick, Vector2>
    {
        public Vector2 Direction { get { return this.param1; } }
    }
}
