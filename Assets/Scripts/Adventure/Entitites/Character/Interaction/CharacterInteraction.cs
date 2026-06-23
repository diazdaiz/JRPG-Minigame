using System.Collections.Generic;
using UnityEngine;

public class CharacterInteraction : MonoBehaviour, Interactable {
    CharacterMovement movement => character.Movement;
    Character character;
    [SerializeField] Transform interactingAreaTransform;
    [SerializeField] Collider2D interactingArea;
    [SerializeField] Collider2D interactedArea;

    private void Awake() {
        character = GetComponent<Character>();
    }

    /// <summary>
    /// Diinteraksi
    /// </summary>
    public void Interact(GameObject interactor) {
        //...
    }

    /// <summary>
    /// Menginteraksi
    /// </summary>
    /// <param name="interactable"></param>
    public void Interact() {
        List<Collider2D> results = new();
        interactingArea.Overlap(new ContactFilter2D { useLayerMask = true, layerMask = LayerMask.GetMask("Interacted Area"), useTriggers = true }, results);
        for (int i = 0; i < results.Count; i++) {
            if (results[i].gameObject == gameObject) continue;
            Interactable interactable = results[i].GetComponent<Interactable>();
            if (interactable == null) continue;
            interactable.Interact(gameObject);
        }
    }

    void Update() {
        if (movement.Direction == CharacterMovement.CharacterDirection.Up) interactingAreaTransform.rotation = Quaternion.Euler(0, 0, 90);
        if (movement.Direction == CharacterMovement.CharacterDirection.Left) interactingAreaTransform.rotation = Quaternion.Euler(0, 0, 180);
        if (movement.Direction == CharacterMovement.CharacterDirection.Down) interactingAreaTransform.rotation = Quaternion.Euler(0, 0, 270);
        if (movement.Direction == CharacterMovement.CharacterDirection.Right) interactingAreaTransform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
