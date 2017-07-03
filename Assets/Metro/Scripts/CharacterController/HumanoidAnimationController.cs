using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace Metro.CharacterController
{
    [RequireComponent(typeof(Humanoid))]
    public sealed class HumanoidAnimationController : MonoBehaviour
    {
        private Humanoid humanoid;

        private CharacterLocomotion locomotion;
        
        private Animator animator;

        private static class AnimatorParameter
        {
            public static readonly int Move = Animator.StringToHash("Move");
        }
        
        private void Start()
        {
            {
                this.humanoid = this.GetComponent<Humanoid>();
                Assert.IsNotNull(this.humanoid);
            }
            {
                this.locomotion = this.GetComponent<CharacterLocomotion>();
            }
            {
                this.animator = this.GetComponentInChildren<Animator>();
                Assert.IsNotNull(this.animator);
            }

            this.locomotion.Velocity
                .SubscribeWithState(this, (v, _this) =>
                {
                    _this.animator.SetFloat(AnimatorParameter.Move, Mathf.Abs(v.x));
                })
                .AddTo(this);
        }
    }
}
