using HK.Framework.EventSystems;
using Metro.Events.Character;
using Metro.Events.InputSystems;
using UnityEngine;
using UniRx;

namespace Metro.CharacterController
{
    [RequireComponent(typeof(Humanoid))]
    public sealed class HumanoidInput : MonoBehaviour
    {
        [SerializeField]
        private float speed;
        
        private Humanoid humanoid;
        
        void Awake()
        {
            this.humanoid = this.GetComponent<Humanoid>();
            
            UniRxEvent.GlobalBroker.Receive<Swipe>()
                .SubscribeWithState(this, (s, _this) =>
                {
                    var velocity = Vector2.right * Mathf.Sign(s.Normalize.x);
                    _this.humanoid.Provider.Publish(Move.Get(velocity, _this.speed));
                })
                .AddTo(this);
            UniRxEvent.GlobalBroker.Receive<Tap>()
                .SubscribeWithState(this, (t, _this) =>
                {
                    _this.humanoid.Provider.Publish(StartFire.Get());
                })
                .AddTo(this);
        }
    }
}
