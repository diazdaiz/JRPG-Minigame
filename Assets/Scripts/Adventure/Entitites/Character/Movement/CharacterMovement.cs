using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {
    public enum CharacterDirection { Up, Left, Down, Right }
    public CharacterDirection Direction { get; set; }
    public Vector2 Velocity {
        get {
            return rb.linearVelocity;
        }
        set {
            rb.linearVelocity = value;
        }
    }
    public float Speed => Velocity.magnitude;
    public float RunSpeed { get; set; } = 6f;
    public float WalkSpeed { get; set; } = 3.5f; //Misal ada walk nanti

    Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        Velocity = Vector2.zero;
        Direction = CharacterDirection.Down;
    }

    public void Move(CharacterDirection direction, bool isRunning = false) {
        //untuk simplicity, skrg selalu lari
        isRunning = true;
        float targetSpeed = isRunning ? RunSpeed : WalkSpeed;
        Velocity = Vector2.zero;
        Direction = direction;
        if (Direction == CharacterDirection.Up) Velocity += Vector2.up;
        if (Direction == CharacterDirection.Left) Velocity += Vector2.left;
        if (Direction == CharacterDirection.Down) Velocity += Vector2.down;
        if (Direction == CharacterDirection.Right) Velocity += Vector2.right;
        Velocity *= targetSpeed;
    }

    public void Move(List<CharacterDirection> directions, bool isRunning = false) {
        //untuk simplicity, skrg selalu lari
        isRunning = true;
        float targetSpeed = isRunning ? RunSpeed : WalkSpeed;
        Velocity = Vector2.zero;
        Direction = directions[0];
        for (int i = 0; i < directions.Count; i++) {
            if (directions[i] == CharacterDirection.Up) Velocity += Vector2.up;
            if (directions[i] == CharacterDirection.Left) Velocity += Vector2.left;
            if (directions[i] == CharacterDirection.Down) Velocity += Vector2.down;
            if (directions[i] == CharacterDirection.Right) Velocity += Vector2.right;
        }
        //nyoba di buat kecepatannya selalu sama
        if (Velocity.magnitude == 0f) return;
        Velocity = Velocity.normalized;
        Velocity *= targetSpeed;
    }

    public void Stop() {
        Velocity = Vector2.zero;
    }
}
