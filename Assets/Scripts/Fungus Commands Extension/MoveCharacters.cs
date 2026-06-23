using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FungusCommandExtension {
    [CommandInfo("Adventure Action", "Move Characters", "Moves characters to its target placement.")]
    [AddComponentMenu("")] // Fungus convention: hide from Unity's Add Component menu
    public class MoveCharacters : Command {
        [System.Serializable]
        class CharacterTargetPlacement {
            [SerializeField] public Character character;
            [SerializeField] public Vector2 position;
            [SerializeField] public CharacterMovement.CharacterDirection direction;
        }

        [SerializeField] List<CharacterTargetPlacement> targets;
        [SerializeField] protected bool waitUntilFinished = true;

        List<NPCCharacterController> nPCCharacterControllers;

        public override void OnEnter() {
            if (targets == null || targets.Count == 0) {
                Continue();
                return;
            }
            nPCCharacterControllers = new();
            for (int i = 0; i < targets.Count; i++) {
                GameObject nPCCharacterControllerGameObject = new GameObject("NPC Character Controller " + (i + 1).ToString());
                nPCCharacterControllerGameObject.transform.parent = transform;
                NPCCharacterController nPCCharacterController = nPCCharacterControllerGameObject.AddComponent<NPCCharacterController>();
                nPCCharacterController.Character = targets[i].character;
                nPCCharacterController.TargetPosition = targets[i].position;
                nPCCharacterController.TargetDirection = targets[i].direction;
            }

            if (waitUntilFinished)
                StartCoroutine(MoveThenContinue());
            else {
                StartCoroutine(MoveRoutine());
                Continue();
            }
        }

        protected virtual IEnumerator MoveThenContinue() {
            yield return StartCoroutine(MoveRoutine());
            Continue();
        }

        protected virtual IEnumerator MoveRoutine() {
            bool allCharactersOnTarget = false;
            while (!allCharactersOnTarget) {
                bool someCharacterOffTarget = false;
                for (int i = 0; i < targets.Count; i++) {
                    CharacterTargetPlacement targetPlacement = targets[i];
                    Character character = targetPlacement.character;
                    Vector3 targetPosition = targetPlacement.position;

                }
                allCharactersOnTarget = !someCharacterOffTarget;
            }

            yield return null;
        }
    }
}