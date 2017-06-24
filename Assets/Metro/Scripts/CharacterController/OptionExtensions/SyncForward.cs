using Metro.Events.Character;
using UniRx;
using UnityEngine;

namespace Metro.CharacterController.OptionExtensions
{
    public sealed class SyncForward : OptionExtension
    {
        public override void Created(Drone drone, Option option)
        {
            drone.Provider.Receive<Move>()
                .Where(m => drone.isActiveAndEnabled)
                .SubscribeWithState(option, (m, _option) =>
                {
                    _option.CachedTransform.rotation = Quaternion.LookRotation(m.Direction, Vector3.right);
                    _option.CachedTransform.rotation *= Quaternion.Euler(0.0f, -90.0f, 0.0f);
                })
                .AddTo(this);
        }
    }
}
