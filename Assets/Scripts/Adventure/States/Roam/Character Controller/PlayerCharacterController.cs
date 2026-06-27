using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour {
    [SerializeField] Character character;
    [SerializeField] bool strictOneDirection = false;
    CharacterMovement movement => character.Movement;
    CharacterInteraction interaction => character.Interaction;
    List<CharacterMovement.CharacterDirection> directions;

    private void Awake() {
        directions = new();
    }

    void MovementControl() {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            if (!directions.Contains(CharacterMovement.CharacterDirection.Up)) {
                directions.Add(CharacterMovement.CharacterDirection.Up);
            }
        }
        else {
            if (directions.Contains(CharacterMovement.CharacterDirection.Up)) {
                directions.Remove(CharacterMovement.CharacterDirection.Up);
            }
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            if (!directions.Contains(CharacterMovement.CharacterDirection.Left)) {
                directions.Add(CharacterMovement.CharacterDirection.Left);
            }
        }
        else {
            if (directions.Contains(CharacterMovement.CharacterDirection.Left)) {
                directions.Remove(CharacterMovement.CharacterDirection.Left);
            }
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            if (!directions.Contains(CharacterMovement.CharacterDirection.Down)) {
                directions.Add(CharacterMovement.CharacterDirection.Down);
            }
        }
        else {
            if (directions.Contains(CharacterMovement.CharacterDirection.Down)) {
                directions.Remove(CharacterMovement.CharacterDirection.Down);
            }
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            if (!directions.Contains(CharacterMovement.CharacterDirection.Right)) {
                directions.Add(CharacterMovement.CharacterDirection.Right);
            }
        }
        else {
            if (directions.Contains(CharacterMovement.CharacterDirection.Right)) {
                directions.Remove(CharacterMovement.CharacterDirection.Right);
            }
        }

        if (directions.Count > 0) {
            if (strictOneDirection) {
                movement.Move(directions[^1]);
            }
            else {
                movement.Move(directions);
            }
        }
        else {
            movement.Stop();
        }
    }

    void InteractionControl() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            interaction.Interact();
        }
    }

    void Update() {
        MovementControl();
        InteractionControl();
    }
}
