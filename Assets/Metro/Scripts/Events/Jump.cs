using HK.Framework.EventSystems;
using UnityEngine;

namespace Metro.Events.Character
{
    public sealed class Jump : Message<Jump, Vector2>
    {
        public Vector2 Direction { get { return this.param1; } }
    }
}
