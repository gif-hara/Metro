using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

namespace Metro.CharacterController
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterLocomotion : MonoBehaviour
    {
        [SerializeField]
        private float speed;
        
        private Rigidbody2D rigidbody2D;

        private Transform cachedTransform;

        private Vector3 velocity;

        void Awake()
        {
            this.rigidbody2D = this.GetComponent<Rigidbody2D>();
            Assert.IsNotNull(this.rigidbody2D);

            this.cachedTransform = this.transform;

            this.UpdateAsObservable()
                .Where(_ => this.isActiveAndEnabled)
                .SubscribeWithState(this, (_, _this) =>
                {
                    this.rigidbody2D.MovePosition(this.cachedTransform.position + this.velocity * Time.deltaTime);
                    this.velocity = Vector3.zero;
                });
        }

        public void Move(Vector3 direction)
        {
            this.velocity += direction * this.speed;
        }
    }
}
