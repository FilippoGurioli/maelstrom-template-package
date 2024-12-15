using System;
using UnityEngine.Events;

namespace Maelstrom.Animation
{
    [Serializable]
    public class AnimationEvent
    {
        public string EventName;
        public UnityEvent OnAnimationEvent;
    }
}
