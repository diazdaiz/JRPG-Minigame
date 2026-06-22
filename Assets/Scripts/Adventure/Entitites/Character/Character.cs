using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, Interactable {
    public CharacterMovement Movement { get; private set; }
    public CharacterInteraction Interaction { get; private set; }

    private void Awake() {
        Movement = GetComponent<CharacterMovement>();
        Interaction = GetComponent<CharacterInteraction>();
    }

    public void Move(CharacterMovement.CharacterDirection direction, bool isRunning) {
        Movement.Move(direction, isRunning);
    }

    public void Move(List<CharacterMovement.CharacterDirection> directions, bool isRunning) {
        Movement.Move(directions, isRunning);
    }

    /// <summary>
    /// Menginteraksi
    /// </summary>
    /// <param name="interactable"></param>
    public void Interact() {
        Interaction.Interact();
    }

    /// <summary>
    /// Diinteraksi
    /// </summary>
    public void Interact(GameObject interactor) {
        Interaction.Interact(interactor);
    }
}
