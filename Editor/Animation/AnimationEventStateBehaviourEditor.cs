using System;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Maelstrom.Animation
{
    [CustomEditor(typeof(AnimationEventStateBehaviour))]
    public class AnimationEventStateBehaviourEditor : Editor
    {
        private AnimationClip _previewClip;
        float _previewTime;
        bool _isPreviewing;

        [MenuItem("GameObject/Enforce T-Pose",false, 0)]
        private static void EnforceTPose()
        {
            var selected = Selection.activeGameObject;
            if (selected == null || !selected.TryGetComponent(out Animator animator) || animator.avatar == null) return;
            var skeletonBones = animator.avatar.humanDescription.skeleton;
            foreach (HumanBodyBones bone in Enum.GetValues(typeof(HumanBodyBones)))
            {
                if (bone == HumanBodyBones.LastBone) continue;
                var boneTransform = animator.GetBoneTransform(bone);
                if (boneTransform == null) continue;
                var skeletonBone = skeletonBones.FirstOrDefault(b => b.name == boneTransform.name);
                if (skeletonBone.name == null) continue;
                if (bone == HumanBodyBones.Hips) boneTransform.localPosition = skeletonBone.position;
                boneTransform.localRotation = skeletonBone.rotation;
            }

            Debug.Log("T-Pose enforced successfully on " + selected.name);
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            AnimationEventStateBehaviour stateBehaviour = target as AnimationEventStateBehaviour;
            if (Validate(stateBehaviour, out string errorMessage))
            {
                GUILayout.Space(10);
                if (_isPreviewing)
                {
                    if (GUILayout.Button("Stop Preview"))
                    {
                        EnforceTPose();
                        _isPreviewing = false;
                    } else 
                    {
                        PreviewAnimationClip(stateBehaviour);
                    }
                } else if (GUILayout.Button("Preview"))
                {
                    _isPreviewing = true;
                }
                GUILayout.Label($"Previewing {_previewClip.name} at {_previewTime:F2}s", EditorStyles.helpBox);
            } else
            {
                EditorGUILayout.HelpBox(errorMessage, MessageType.Error);
            }
        }

        private void PreviewAnimationClip(AnimationEventStateBehaviour stateBehaviour)
        {
            if (_previewClip == null) return;
            _previewTime = stateBehaviour.TriggerTime * _previewClip.length;
            AnimationMode.StartAnimationMode();
            AnimationMode.SampleAnimationClip(Selection.activeGameObject, _previewClip, _previewTime);
            AnimationMode.StopAnimationMode();
        }

        private bool Validate(AnimationEventStateBehaviour stateBehaviour, out string errorMessage)
        {
            var animatorController = GetValidAnimatorController(out errorMessage);
            if (animatorController == null) return false;
            ChildAnimatorState matchingState = animatorController.layers
                .SelectMany(l => l.stateMachine.states)
                .FirstOrDefault(s => s.state.behaviours.Contains(stateBehaviour));
            _previewClip = matchingState.state?.motion as AnimationClip;
            if (_previewClip == null)
            {
                errorMessage = "State has no AnimationClip";
                return false;
            }
            return true;
        }

        private AnimatorController GetValidAnimatorController(out string errorMessage)
        {
            errorMessage = string.Empty;
            GameObject targetGameObject = Selection.activeGameObject;
            if (targetGameObject == null)
            {
                errorMessage = "No GameObject selected";
                return null;
            }
            Animator animator = targetGameObject.GetComponent<Animator>();
            if (animator == null)
            {
                errorMessage = "Selected GameObject has no Animator component";
                return null;
            }
            AnimatorController animatorController = animator.runtimeAnimatorController as AnimatorController;
            if (animatorController == null)
            {
                errorMessage = "Selected Animator has no AnimatorController";
                return null;
            }
            return animatorController;
        }
    }
}
