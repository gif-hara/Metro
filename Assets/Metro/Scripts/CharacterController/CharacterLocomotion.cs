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
        private float gravityScale;

        [SerializeField]
        private ContactFilter2D contactFilter;

        private Rigidbody2D cachedRigidbody2D;

        private ReactiveProperty<Vector2> velocity = new ReactiveProperty<Vector2>();

        private ReactiveProperty<Vector2> force = new ReactiveProperty<Vector2>();

        public IReadOnlyReactiveProperty<Vector2> Velocity
        {
            get { return this.velocity; }
        }

        void Awake()
        {
            this.cachedRigidbody2D = this.GetComponent<Rigidbody2D>();
            Assert.IsNotNull(this.cachedRigidbody2D);

            var fixedUpdate = this.FixedUpdateAsObservable();
            fixedUpdate
                .Where(_ => this.isActiveAndEnabled)
                .SubscribeWithState(this, (_, _this) =>
                {
                    var r = _this.cachedRigidbody2D;
                    var v = Vector2.zero;
                    if(_this.velocity.Value.sqrMagnitude > 0.0f)
                    {
                        v += _this.velocity.Value;
                        _this.velocity.Value = Vector3.zero;
                    }
                    if(_this.force.Value.sqrMagnitude > 0.0f)
                    {
                        v += _this.force.Value;
                    }
                    _this.force.Value += (Physics2D.gravity * _this.gravityScale) * Time.deltaTime;
                    if(_this.force.Value.y < 0.0f && _this.cachedRigidbody2D.IsTouching(_this.contactFilter))
                    {
                        _this.force.Value = Vector2.zero;
                    }
                    r.MovePosition(r.position + v * Time.deltaTime);
                });
        }

        public void Move(Vector2 velocity)
        {
            this.velocity.Value += velocity;
        }

        public void AddForce(Vector2 force)
        {
            this.force.Value = force;
        }
    }
}
