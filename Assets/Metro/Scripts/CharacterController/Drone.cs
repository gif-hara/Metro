using UnityEngine;

namespace Metro.CharacterController
{
    public class Drone : MonoBehaviour
    {
        [SerializeField]
        private GameObject model;

        private Transform cachedTransform;

        void Awake()
        {
            this.cachedTransform = this.transform;
            var modelInstance = Instantiate(this.model, this.cachedTransform);
        }
    }
}
