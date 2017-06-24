using Metro.CharacterController.OptionExtensions;
using Metro.Events.Character;
using UniRx;
using UnityEngine;

namespace Metro.CharacterController
{
    public sealed class Option : MonoBehaviour
    {
        [SerializeField]
        private GameObject model;

        [SerializeField]
        private Transform modelParent;

        [SerializeField]
        private Muzzle muzzle;
        
        public Transform CachedTransform { private set; get; }

        void Awake()
        {
            this.CachedTransform = this.transform;
        }

        public void Create(Drone drone)
        {
            var instance = Instantiate(this, drone.CachedTransform);
            instance.Initialize(drone);
        }

        private void Initialize(Drone drone)
        {
            this.transform.localPosition = Vector3.zero;
            var modelInstance = Instantiate(this.model, this.modelParent);
            modelInstance.transform.localPosition = Vector3.zero;
            modelInstance.transform.localRotation = Quaternion.identity;

            drone.Provider.Receive<StartFire>()
                .Where(_ => drone.isActiveAndEnabled)
                .SubscribeWithState(this, (s, _this) =>
                {
                    _this.muzzle.Fire();
                })
                .AddTo(this);

            var extensions = this.GetComponents<OptionExtension>();
            for (int i = 0; i < extensions.Length; i++)
            {
                extensions[i].Created(drone, this);
            }
        }
    }
}
