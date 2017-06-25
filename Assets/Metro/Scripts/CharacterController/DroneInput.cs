using Metro.Events.Character;
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
