using HK.Framework.EventSystems;
using HK.Framework.Extensions;
using Metro.Events.Character;
using Metro.Events.InputSystems;
using UnityEngine;
using UniRx;
using UnityEngine.Assertions.Must;

namespace Metro.CharacterController
{
    [RequireComponent(typeof(Humanoid))]
    public sealed class HumanoidInput : MonoBehaviour
    {
        [SerializeField]
        private float speed;

        [SerializeField]
        private float jumpRange;
        
        private Humanoid humanoid;
        
        void Awake()
        {
            this.humanoid = this.GetComponent<Humanoid>();
            
            Broker.Global.Receive<Swipe>()
                .SubscribeWithState(this, (s, _this) =>
                {
                    var velocity = Vector2.right * Mathf.Sign(s.Normalize.x);
                    _this.humanoid.Provider.Publish(Move.Get(velocity, _this.speed));
                })
                .AddTo(this);
            Broker.Global.Receive<Tap>()
                .SubscribeWithState(this, (t, _this) =>
                {
                    _this.humanoid.Provider.Publish(Jump.Get(Vector2.up));
                })
                .AddTo(this);
        }

        private bool CanJump(float angle)
        {
            return angle > (90 - this.jumpRange) && angle < (90 + this.jumpRange);
        }
    }
}
