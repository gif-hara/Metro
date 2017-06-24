using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets._2D;

namespace Metro.CharacterController
{
    [RequireComponent(typeof(PlatformerCharacter2D))]
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
        
        private PlatformerCharacter2D character;

        void Awake()
        {
            this.character = GetComponent<PlatformerCharacter2D>();
            Assert.IsNotNull(this.character);
        }
    
        void Update()
        {
            var horizontal = CrossPlatformInputManager.GetAxis(this.horizontalName);
            this.character.Move(horizontal, false, false);

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
