using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets._2D;

namespace Metro.CharacterController
{
    [RequireComponent(typeof(CharacterLocomotion))]
    public class CharacterControllInput : MonoBehaviour
    {
        [SerializeField]
        private string horizontalName = "Horizontal";

        [SerializeField]
        private string verticalName = "Vertical";

        [SerializeField]
        private string submitName = "Submit";

        [SerializeField]
        private List<Muzzle> muzzles;
        
        private CharacterLocomotion locomotion;

        void Awake()
        {
            this.locomotion = GetComponent<CharacterLocomotion>();
            Assert.IsNotNull(this.locomotion);
        }
    
        void Update()
        {
            var horizontal = CrossPlatformInputManager.GetAxis(this.horizontalName);
            this.locomotion.Move(Vector3.right * horizontal);

            if (CrossPlatformInputManager.GetButtonDown(this.submitName))
            {
                for (int i = 0; i < this.muzzles.Count; i++)
                {
                    this.muzzles[i].Fire();
                }
            }
        }
    }
}
