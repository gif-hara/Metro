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

        private AnimatorLayer layerInfo = null;

        private static class AnimatorParameter
        {
            public static readonly int Move = Animator.StringToHash("Move");
            public static readonly int AimLayer = Animator.StringToHash("Aim Layer");
        }

        private class AnimatorLayer
        {
            public static readonly string BaseLayerName = "Base Layer";
            
            public static readonly string AimLayerName = "Aim Layer";

            public readonly int BaseLayer;
            
            public readonly int AimLayer;

            public AnimatorLayer(Animator animator)
            {
                Assert.IsNotNull(animator);
                this.BaseLayer = animator.GetLayerIndex(BaseLayerName);
                this.AimLayer = animator.GetLayerIndex(AimLayerName);
            }
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
            {
                this.layerInfo = new AnimatorLayer(this.animator);
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
                    _this.animator.SetLayerWeight(_this.layerInfo.AimLayer, 1.0f);
                    if (_this.unAimTweener != null)
                    {
                        _this.unAimTweener.Kill();
                    }
                    _this.unAimTweener = DOTween.To(
                        () => 1.0f,
                        f =>
                    {
                        _this.animator.SetLayerWeight(_this.layerInfo.AimLayer, f);
                    },
                        0.0f,
                        0.5f)
                        .SetDelay(1.0f);
                })
                .AddTo(this);
                
        }
    }
}
