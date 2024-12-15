using System.Collections.Generic;
using UnityEngine;

namespace Maelstrom.Animation
{
    [AddComponentMenu("Maelstrom/Animation/AnimationEventReceiver")]
    public class AnimationEventReceiver : MonoBehaviour
    {
        [SerializeField] List<AnimationEvent> _animationEvents = new();

        public void OnAnimationEventTriggered(string eventName)
        {
            AnimationEvent matchingEvent = _animationEvents.Find(se => se.EventName == eventName);
            matchingEvent?.OnAnimationEvent?.Invoke();
        }
    }
}