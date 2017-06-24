using UnityEngine;

namespace Metro.CharacterController
{
    public sealed class Option : MonoBehaviour
    {
        [SerializeField]
        private GameObject model;

        [SerializeField]
        private GameObject modelParent;

        [SerializeField]
        private Muzzle muzzle;

        public void Create(Drone drone)
        {
            var instance = Instantiate(this, drone.CachedTransform);
        }
    }
}
