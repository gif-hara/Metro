using System.Collections.Generic;
using Metro.Events.Character;
using UnityEngine;
using UnityEngine.Assertions;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets._2D;

namespace Metro.CharacterController
{
    [RequireComponent(typeof(Drone))]
    [RequireComponent(typeof(CharacterLocomotion))]
    public class CharacterControllInput : MonoBehaviour
    {
        [SerializeField]
        private string horizontalName = "Horizontal";

        [SerializeField]
        private string verticalName = "Vertical";

        [SerializeField]
        private string submitName = "Submit";

        private CharacterLocomotion locomotion;

        private Drone drone;

        void Awake()
        {
            this.locomotion = this.GetComponent<CharacterLocomotion>();
            Assert.IsNotNull(this.locomotion);

            this.drone = this.GetComponent<Drone>();
            Assert.IsNotNull(this.drone);
        }
    
        void Update()
        {
            var horizontal = CrossPlatformInputManager.GetAxis(this.horizontalName);
            var vertical = CrossPlatformInputManager.GetAxis(this.verticalName);
            this.locomotion.Move(new Vector2(horizontal, vertical));

            if (CrossPlatformInputManager.GetButtonDown(this.submitName))
            {
                this.drone.Provider.Publish(StartFire.Get());
            }
        }
    }
}
