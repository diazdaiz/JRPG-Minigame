using System.Collections.Generic;
using UnityEngine;

public class NPCCharacterController : MonoBehaviour {
    public Vector2 TargetPosition { get; set; }
    public CharacterMovement.CharacterDirection TargetDirection { get; set; }
    public Character Character {
        get {
            return character;
        }
        set {
            character = value;
        }
    }

    [SerializeField] Character character;

    void Update() {
        List<CharacterMovement.CharacterDirection> directions = new();
        float dx = TargetPosition.x - character.transform.position.x;
        float dy = TargetPosition.y - character.transform.position.y;
        if (Mathf.Abs(dx) > 0.1f) {
            if (dx > 0.1f) directions.Add(CharacterMovement.CharacterDirection.Right);
            if (dx < -0.1f) directions.Add(CharacterMovement.CharacterDirection.Left);
        }
        if (Mathf.Abs(dy) > 0.1f) {
            if (dy > 0.1f) directions.Add(CharacterMovement.CharacterDirection.Up);
            if (dy < -0.1f) directions.Add(CharacterMovement.CharacterDirection.Down);
        }
        Character.Move(directions, true);
        if (directions.Count > 0) {
            character.Movement.Direction = TargetDirection;
        }
    }
}
