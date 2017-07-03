using UnityEngine;

namespace Metro.CharacterController.OptionExtensions
{
    public abstract class OptionExtension : MonoBehaviour
    {
        public abstract void Created(Humanoid humanoid, Option option);
    }
}
