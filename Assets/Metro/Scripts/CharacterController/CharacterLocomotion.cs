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

        private Vector2 velocity;

        void Awake()
        {
            this.rigidbody2D = this.GetComponent<Rigidbody2D>();
            Assert.IsNotNull(this.rigidbody2D);

            this.cachedTransform = this.transform;

            this.UpdateAsObservable()
                .Where(_ => this.isActiveAndEnabled)
                .Where(_ => this.velocity.sqrMagnitude > 0.0f)
                .SubscribeWithState(this, (_, _this) =>
                {
                    this.rigidbody2D.position += this.velocity * Time.deltaTime;
                    this.velocity = Vector3.zero;
                });
        }

        public void Move(Vector2 direction)
        {
            this.velocity += direction * this.speed;
        }
    }
}
