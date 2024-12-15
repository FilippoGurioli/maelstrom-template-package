using UnityEngine;

namespace Maelstrom.Interaction
{
    /// <summary>
    /// Interface for interactable objects.
    /// </summary>
    public interface IInteractable
    {
        /// <summary>
        /// Interact with the object.
        /// </summary>
        /// <param name="interactor">The object that is interacting with this object.</param>
        void Interact(GameObject interactor);
    }
}