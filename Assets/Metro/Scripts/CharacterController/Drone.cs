using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Metro.CharacterController
{
    public class Drone : MonoBehaviour
    {
        [SerializeField]
        private GameObject model;

        [SerializeField]
        private List<Option> options;

        public Transform CachedTransform { private set; get; }
        
        public IMessageBroker Provider { private set; get; }

        void Awake()
        {
            this.CachedTransform = this.transform;
            var modelInstance = Instantiate(this.model, this.CachedTransform);
            
            this.Provider = new MessageBroker();

            for (int i = 0; i < this.options.Count; i++)
            {
                this.options[i].Create(this);
            }
        }
    }
}
