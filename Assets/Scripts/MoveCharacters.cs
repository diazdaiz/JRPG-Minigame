using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CommandInfo("RoamAction",
             "Move Characters",
             "Moves characters according to a Roam config asset.")]
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

    public override void OnEnter() {
        if (targets == null || targets.Count == 0) {
            Continue();
            return;
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
                List<CharacterMovement.CharacterDirection> directions = new();
                float dx = targetPosition.x - character.transform.position.x;
                float dy = targetPosition.y - character.transform.position.y;
                if (Mathf.Abs(dx) > 0.1f) {
                    if (dx > 0.1f) directions.Add(CharacterMovement.CharacterDirection.Right);
                    if (dx < -0.1f) directions.Add(CharacterMovement.CharacterDirection.Left);
                }
                if (Mathf.Abs(dy) > 0.1f) {
                    if (dy > 0.1f) directions.Add(CharacterMovement.CharacterDirection.Up);
                    if (dy < -0.1f) directions.Add(CharacterMovement.CharacterDirection.Down);
                }
                if (directions.Count > 0) {
                    someCharacterOffTarget = true;
                    character.Movement.Direction = targetPlacement.direction;
                }
            }
            allCharactersOnTarget = !someCharacterOffTarget;
        }

        yield return null;
    }
}