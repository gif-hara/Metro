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

        public void Create(Drone drone)
        {
            var instance = Instantiate(this, drone.CachedTransform);
            instance.transform.localPosition = Vector3.zero;
            var modelInstance = Instantiate(instance.model, instance.modelParent);
            modelInstance.transform.localPosition = Vector3.zero;

            drone.Provider.Receive<StartFire>()
                .Where(_ => drone.isActiveAndEnabled)
                .SubscribeWithState(instance, (s, _instance) =>
                {
                    _instance.muzzle.Fire();
                })
                .AddTo(this);
        }
    }
}
