using Metro.Events.Character;
using UniRx;
using UnityEngine;

namespace Metro.CharacterController.OptionExtensions
{
    public sealed class SyncForward : OptionExtension
    {
        private readonly float Sensitivity = 10.0f;

        private float angle;

        public override void Created(Drone drone, Option option)
        {
            drone.Provider.Receive<Move>()
                .Where(m => drone.isActiveAndEnabled)
                .SubscribeWithState2(this, option, (m, _this, _option) =>
                {
                    var target = Mathf.Atan2(m.Direction.y, m.Direction.x) * Mathf.Rad2Deg;
                    _this.angle = Mathf.LerpAngle(_this.angle, target, Sensitivity * Time.deltaTime);
                    _option.CachedTransform.rotation = Quaternion.AngleAxis(_this.angle, Vector3.forward);
                })
                .AddTo(this);
        }
    }
}
