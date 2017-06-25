using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

namespace Metro.CharacterController
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterLocomotion : MonoBehaviour
    {
        private Rigidbody2D cachedRigidbody2D;

        private Vector2 velocity;

        void Awake()
        {
            this.cachedRigidbody2D = this.GetComponent<Rigidbody2D>();
            Assert.IsNotNull(this.cachedRigidbody2D);

            this.UpdateAsObservable()
                .Where(_ => this.isActiveAndEnabled)
                .Where(_ => this.velocity.sqrMagnitude > 0.0f)
                .SubscribeWithState(this, (_, _this) =>
                {
                    this.cachedRigidbody2D.position += this.velocity * Time.deltaTime;
                    this.velocity = Vector3.zero;
                });
        }

        public void Move(Vector2 velocity)
        {
            this.velocity += velocity;
        }
    }
}
