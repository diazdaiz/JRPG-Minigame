using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteraction : InteractableObject {
    CharacterMovement Movement => character.Movement;

    [SerializeField] Character character;
    [SerializeField] Transform interactingAreaTransform;
    [SerializeField] Collider2D interactingArea;
    [SerializeField] Collider2D interactedArea;
    Block block;
    AdventureManager.AdventureState previousAdventureState;

    /// <summary>
    /// Menginteraksi
    /// </summary>
    /// <param name="interactable"></param>
    public void Interact() {
        List<Collider2D> results = new();
        interactingArea.Overlap(new ContactFilter2D { useLayerMask = true, layerMask = LayerMask.GetMask("Interacted Area"), useTriggers = true }, results);
        InteractableObject closestInteractable = null;
        float closestDistance = 9999;
        for (int i = 0; i < results.Count; i++) {
            if (results[i].gameObject == gameObject) continue;
            InteractableObject interactable = results[i].GetComponent<InteractableObject>();
            if (interactable == null) continue;
            if ((results[i].transform.position - transform.position).magnitude >= closestDistance) continue;
            closestInteractable = interactable;
            closestDistance = (results[i].transform.position - transform.position).magnitude;
        }
        if (closestInteractable != null) {
            Interaction interaction = closestInteractable.Interact(gameObject);
            GameObject parentObject = closestInteractable.transform.parent.gameObject;

            if (interaction.Type == Interaction.InteractionType.Cutscene || interaction.Type == Interaction.InteractionType.Dialogue) {
                block = Flowchart != null ? Flowchart.FindBlock(interaction.BlockName) : null;
                Adventure.ChangeState(AdventureManager.AdventureState.Cutscene);
                Flowchart.ExecuteBlock(block);
                previousAdventureState = Adventure.State;
                StartCoroutine(CheckForCutsceneEnding());
            }
            else if (interaction.Type == Interaction.InteractionType.Collect) {
                if (interaction.CollectInteractionType == Interaction.CollectType.Pick) {
                    Item item = parentObject.GetComponent<Item>();
                    parentObject.SetActive(false);
                    item.gameObject.SetActive(false);
                    character.PickItem(item);
                }
                else if (interaction.CollectInteractionType == Interaction.CollectType.Chop) {
                    Item item = interaction.CollactableItem;
                    character.UseItem(typeof(Axe), closestInteractable);
                }
                else if (interaction.CollectInteractionType == Interaction.CollectType.Mine) {
                    Item item = interaction.CollactableItem;
                    character.UseItem(typeof(Pickaxe), closestInteractable);
                }
            }
            else if (interaction.Type == Interaction.InteractionType.EnterPortal) {
                Portal portal = parentObject.GetComponent<Portal>();
                portal.Enter(gameObject);
            }
        }
    }

    IEnumerator CheckForCutsceneEnding() {
        while (block.IsExecuting()) {
            yield return null;
        }
        Adventure.ChangeState(previousAdventureState);
    }

    void Update() {
        if (Movement.Direction == CharacterMovement.CharacterDirection.Up) interactingAreaTransform.rotation = Quaternion.Euler(0, 0, 90);
        if (Movement.Direction == CharacterMovement.CharacterDirection.Left) interactingAreaTransform.rotation = Quaternion.Euler(0, 0, 180);
        if (Movement.Direction == CharacterMovement.CharacterDirection.Down) interactingAreaTransform.rotation = Quaternion.Euler(0, 0, 270);
        if (Movement.Direction == CharacterMovement.CharacterDirection.Right) interactingAreaTransform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
