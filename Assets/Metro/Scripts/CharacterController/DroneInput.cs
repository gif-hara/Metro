using HK.Framework.EventSystems;
using Metro.Events.Character;
using Metro.Events.InputSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityStandardAssets.CrossPlatformInput;

namespace Metro.CharacterController
{
    [RequireComponent(typeof(Drone))]
    public class DroneInput : MonoBehaviour
    {
        [SerializeField]
        private string horizontalName = "Horizontal";

        [SerializeField]
        private string verticalName = "Vertical";

        [SerializeField]
        private string submitName = "Submit";

        // FIXME:
        [SerializeField]
        private float speed;

        private Drone drone;

        private Vector2 cachedVelocity;

        void Awake()
        {
            this.drone = this.GetComponent<Drone>();
            Assert.IsNotNull(this.drone);

            UniRxEvent.GlobalBroker.Receive<Swipe>()
                .SubscribeWithState(this, (s, _this) =>
                {
                    _this.drone.Provider.Publish(Move.Get(s.Normalize, _this.speed));
                })
                .AddTo(this);
            UniRxEvent.GlobalBroker.Receive<Tap>()
                .SubscribeWithState(this, (t, _this) =>
                {
                    _this.drone.Provider.Publish(StartFire.Get());
                })
                .AddTo(this);
        }
    
        void Update()
        {
            var horizontal = CrossPlatformInputManager.GetAxis(this.horizontalName);
            var vertical = CrossPlatformInputManager.GetAxis(this.verticalName);
            this.cachedVelocity.Set(horizontal, vertical);

            if (this.cachedVelocity.sqrMagnitude > 0.0f)
            {
                this.drone.Provider.Publish(Move.Get(this.cachedVelocity, this.speed));
            }

            if (CrossPlatformInputManager.GetButtonDown(this.submitName))
            {
                this.drone.Provider.Publish(StartFire.Get());
            }
        }
    }
}
