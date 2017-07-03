using System.Collections.Generic;
using Metro.Events.Character;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace Metro.CharacterController
{
    [RequireComponent(typeof(CharacterLocomotion))]
    public class Humanoid : MonoBehaviour
    {
        [SerializeField]
        private GameObject model;

        [SerializeField]
        private List<Option> options;

        public Transform CachedTransform { private set; get; }
        
        public IMessageBroker Provider { private set; get; }
        
        public CharacterLocomotion Locomotion { private set; get; }

        void Awake()
        {
            this.CachedTransform = this.transform;
            var modelInstance = Instantiate(this.model, this.CachedTransform);
            modelInstance.transform.localPosition = Vector3.zero;
            
            this.Provider = new MessageBroker();

            this.Locomotion = this.GetComponent<CharacterLocomotion>();
            Assert.IsNotNull(this.Locomotion);

            for (int i = 0; i < this.options.Count; i++)
            {
                this.options[i].Create(this);
            }

            this.Provider.Receive<Move>()
                .Where(m => this.isActiveAndEnabled)
                .SubscribeWithState(this, (m, _this) =>
                {
                    _this.Locomotion.Move(m.Direction * m.Speed);
                    var angle = m.Direction.x > 0 ? 0.0f : 180.0f;
                    _this.CachedTransform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
                })
                .AddTo(this);
        }
    }
}
