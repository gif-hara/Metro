using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;
using UniRx;

namespace Metro
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class AutoFireController : MonoBehaviour
    {
        void Start()
        {
            this.OnTriggerEnter2DAsObservable()
                .Subscribe(c =>
                {
                    Debug.Log(c.name);
                });
        }
    }
}
