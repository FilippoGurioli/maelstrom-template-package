using System.Linq;
using UnityEngine;

namespace Maelstrom.Interaction
{
    /// <summary>
    /// Interactor component for PlayerInput.
    /// </summary>
    [AddComponentMenu("Maelstrom/Interaction/Interactor")]
    public class Interactor : MonoBehaviour
    {
        [SerializeField] LayerMask _interactableLayer;
        [SerializeField] float _detectionDistance = 1f;
        [SerializeField, Min(0)] float _size = 1f;

        public void Interact()
        {
            var hit = Physics.OverlapBox(
                transform.position + transform.forward * _detectionDistance, 
                Vector3.one * _size / 2, 
                Quaternion.identity,
                _interactableLayer
            );
            if (hit.Length == 0) return;
            var interactable = hit.FirstOrDefault(h => h.GetComponentInParent<IInteractable>() != null);
            if (interactable != null)
            {
                interactable.GetComponentInParent<IInteractable>().Interact(gameObject);
            }
        }
    }
}