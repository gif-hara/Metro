using System.Collections.Generic;
using System.Text;
using HK.Framework.EventSystems;
using HK.Framework.Text;
using Metro.Events.InputSystems;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using UniRx;

namespace Metro.UI
{
    [RequireComponent(typeof(Text))]
    public sealed class InputLogUIController : MonoBehaviour
    {
        [SerializeField]
        private StringAsset.Finder tapMessage;

        [SerializeField]
        private float fontSize;
        
        private Text cachedText;

        private StringBuilder builder = new StringBuilder();

        private List<string> logs = new List<string>();

        private int displayNumber;

        void Awake()
        {
            {
                this.cachedText = this.GetComponent<Text>();
                Assert.IsNotNull(this.cachedText);
            }
            {
                UniRxEvent.GlobalBroker.Receive<Tap>()
                    .Subscribe(t => this.AddText(this.CreateTapMessage(t.ScreenPosition)))
                    .AddTo(this);
            }
            {
                var rectTransform = this.transform as RectTransform;
                this.displayNumber = Mathf.FloorToInt(rectTransform.rect.height / this.fontSize);
            }
        }

        private void AddText(string message)
        {
            this.logs.Add(message);
            this.DisplayLog();
        }

        private void DisplayLog()
        {
            this.builder.Length = 0;
            var offset = Mathf.Max(0, this.logs.Count - this.displayNumber);
            var end = Mathf.Min(this.logs.Count, this.displayNumber);
            for (var i = 0; i < end; i++)
            {
                this.builder.AppendLine(this.logs[i + offset]);
            }
            
            this.cachedText.text = this.builder.ToString();
        }

        private string CreateTapMessage(Vector2 screenPosition)
        {
            return this.tapMessage.Format(screenPosition);
        }
    }
}
