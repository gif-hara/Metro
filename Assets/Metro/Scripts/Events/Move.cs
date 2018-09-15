using HK.Framework.EventSystems;
using UnityEngine;

namespace Metro.Events.Character
{
    public sealed class Move : Message<Move, Vector2, float>
    {
        public Vector2 Direction{ get { return this.param1; } }
        
        public float Speed{ get { return this.param2; } }
    }
}
