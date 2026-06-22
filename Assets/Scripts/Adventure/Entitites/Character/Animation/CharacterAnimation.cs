using UnityEngine;

public class CharacterAnimation : MonoBehaviour {
    CharacterMovement movement => character.Movement;
    SpriteRenderer spriteRenderer;
    [SerializeField] Character character;
    [SerializeField] Sprite up;
    [SerializeField] Sprite left;
    [SerializeField] Sprite down;
    [SerializeField] Sprite right;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        if (movement.Direction == CharacterMovement.CharacterDirection.Up) spriteRenderer.sprite = up;
        if (movement.Direction == CharacterMovement.CharacterDirection.Left) spriteRenderer.sprite = left;
        if (movement.Direction == CharacterMovement.CharacterDirection.Down) spriteRenderer.sprite = down;
        if (movement.Direction == CharacterMovement.CharacterDirection.Right) spriteRenderer.sprite = right;
    }
}
