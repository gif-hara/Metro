using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;
using HK.Framework.EventSystems;
using Metro.Events.InputSystems;

namespace Metro.InputSystems
{
    /// <summary>
    /// スタンドアロンによる入力を制御するクラス
    /// </summary>
    public sealed class StandaloneInputController : MonoBehaviour
    {
        public const string Horizontal = "Horizontal";

        public const string Vertical = "Vertical";

        public const string Jump = "Jump";

        void Start()
        {
            this.UpdateAsObservable()
                .Select(_ => new Vector2(Input.GetAxis(Horizontal), Input.GetAxis(Vertical)).normalized)
                .Where(m => m.sqrMagnitude > 0.0f)
                .Subscribe(m => Broker.Global.Publish(Swipe.Get(m)));

            this.UpdateAsObservable()
                .Select(_ => Input.GetAxisRaw(Jump))
                .DistinctUntilChanged()
                .Where(j => j > 0.0f)
                .Subscribe(j => Broker.Global.Publish(Tap.Get(Vector2.zero)));
        }
    }
}
