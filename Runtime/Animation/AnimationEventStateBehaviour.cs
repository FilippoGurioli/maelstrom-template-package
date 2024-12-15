using UnityEngine;

namespace Maelstrom.Animation
{
    public class AnimationEventStateBehaviour : StateMachineBehaviour
    {
        [SerializeField] private string _eventName;
        [SerializeField, Range(0f, 1f)] private float _triggerTime;
        [SerializeField, ReadOnly] private bool _hasTriggered;

        public float TriggerTime => _triggerTime;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _hasTriggered = false;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            float currentTime = stateInfo.normalizedTime % 1f;
            if (!_hasTriggered && currentTime >= _triggerTime)
            {
                NotifyReceiver(animator);
                _hasTriggered = true;
            }
        }

        private void NotifyReceiver(Animator animator)
        {
            AnimationEventReceiver receiver = animator.GetComponent<AnimationEventReceiver>();
            receiver?.OnAnimationEventTriggered(_eventName);
        }
    }
}
