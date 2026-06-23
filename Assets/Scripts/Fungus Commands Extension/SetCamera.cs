using Fungus;
using NaughtyAttributes;
using System.Collections;
using UnityEngine;

namespace FungusCommandExtension {
    [CommandInfo("Adventure Action", "Set Camera", "Set preferable camera.")]
    [AddComponentMenu("")] // Fungus convention: hide from Unity's Add Component menu
    public class SetCamera : Command {
        CameraManager Camera => Game.Camera;
        [SerializeField] CameraManager.CameraMovement movement;
        [SerializeField] float CameraSpeed = 6;
        [SerializeField] CameraManager.CameraRule rule;

        [AllowNesting][ShowIf(nameof(rule), CameraManager.CameraRule.TargetPosition)] public bool waitUntilFinished = true;
        [AllowNesting][ShowIf(nameof(rule), CameraManager.CameraRule.TargetPosition)] public Vector2 targetPosition;
        [AllowNesting][ShowIf(nameof(rule), CameraManager.CameraRule.TargetPosition)] public float finishedWhenDistanceLessThen = 3;

        public override void OnEnter() {
            Camera.Movement = movement;
            Camera.CameraSpeedMultiplier = CameraSpeed;
            Camera.Rule = rule;
            if (Camera.Rule == CameraManager.CameraRule.TargetPosition) {
                Camera.TargetPosition = targetPosition;
                if (waitUntilFinished) {
                    StartCoroutine(CheckForMovement());
                }
                else {
                    Continue();
                }
            }
            else {
                Continue();
            }
        }

        IEnumerator CheckForMovement() {
            while ((new Vector2(Camera.transform.position.x, Camera.transform.position.y) - new Vector2(targetPosition.x, targetPosition.y)).magnitude > finishedWhenDistanceLessThen) {
                yield return null;
            }
            Continue();
        }
    }
}
