using DG.Tweening;
using Metro.Events.Character;
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

        private Tweener unAimTweener = null;

        private static class AnimatorParameter
        {
            public static readonly int Move = Animator.StringToHash("Move");
            public static readonly int AimLayer = Animator.StringToHash("Aim Layer");
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
            this.humanoid.Provider.Receive<StartFire>()
                .Where(s => this.isActiveAndEnabled)
                .SubscribeWithState(this, (s, _this) =>
                {
                    var layerIndex = _this.animator.GetLayerIndex("Aim Layer");
                    _this.animator.SetLayerWeight(layerIndex, 1.0f);
                    if (_this.unAimTweener != null)
                    {
                        _this.unAimTweener.Kill();
                    }
                    _this.unAimTweener = DOTween.To(
                        () => 1.0f,
                        f =>
                    {
                        _this.animator.SetLayerWeight(layerIndex, f);
                    },
                        0.0f,
                        0.5f)
                        .SetDelay(1.0f);
                })
                .AddTo(this);
                
        }
    }
}
