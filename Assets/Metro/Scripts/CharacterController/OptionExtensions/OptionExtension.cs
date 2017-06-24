using UnityEngine;

namespace Metro.CharacterController.OptionExtensions
{
    public abstract class OptionExtension : MonoBehaviour
    {
        public abstract void Created(Drone drone, Option option);
    }
}
