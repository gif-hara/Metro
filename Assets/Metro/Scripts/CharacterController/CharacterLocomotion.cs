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

        private ReactiveProperty<Vector2> velocity = new ReactiveProperty<Vector2>();

        public IReadOnlyReactiveProperty<Vector2> Velocity
        {
            get { return this.velocity; }
        }

        void Awake()
        {
            this.cachedRigidbody2D = this.GetComponent<Rigidbody2D>();
            Assert.IsNotNull(this.cachedRigidbody2D);

            this.UpdateAsObservable()
                .Where(_ => this.isActiveAndEnabled)
                .Where(_ => this.velocity.Value.sqrMagnitude > 0.0f)
                .SubscribeWithState(this, (_, _this) =>
                {
                    this.cachedRigidbody2D.position += this.velocity.Value * Time.deltaTime;
                    this.velocity.Value = Vector3.zero;
                });
        }

        public void Move(Vector2 velocity)
        {
            this.velocity.Value += velocity;
        }
    }
}
