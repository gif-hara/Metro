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
}
