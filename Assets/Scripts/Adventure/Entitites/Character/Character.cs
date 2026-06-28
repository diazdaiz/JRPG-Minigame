using System;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
    public CharacterMovement Movement => movement;
    public CharacterInteraction Interaction => interaction;
    public CharacterInventory Inventory => inventory;
    public CharacterCombat Combat => combat;

    [SerializeField] CharacterInteraction interaction;
    [SerializeField] CharacterMovement movement;
    [SerializeField] CharacterInventory inventory;
    [SerializeField] CharacterCombat combat;

    public void Move(CharacterMovement.CharacterDirection direction, bool isRunning) {
        Movement.Move(direction, isRunning);
    }

    public void Move(List<CharacterMovement.CharacterDirection> directions, bool isRunning) {
        Movement.Move(directions, isRunning);
    }

    public void GiveItem(Item item, Character character) {
        Debug.Log(Inventory.Items.Contains(item));
        if (Inventory.Items.Contains(item)) {
            character.ReceiveItem(item);
            Inventory.Items.Remove(item);
        }
    }

    public void ReceiveItem(Item item) {
        Inventory.Items.Add(item);
    }

    public void PickItem(Item item) {
        Inventory.Items.Add(item);
    }

    public void ThrowItem(Item item) {
        Inventory.Items.Remove(item);
    }

    public void UseItem(Item item, InteractableObject on) {
        item.Use(on);
    }

    public void UseItem(Type itemType, InteractableObject on) {
        for (int i = 0; i < Inventory.Items.Count; i++) {
            Item item = Inventory.Items[i];
            if (item.GetType() == itemType) {
                item.Use(on);
                return;
            }
        }
    }

    public void EquipItem(Type itemType) {
        for (int i = 0; i < Inventory.Items.Count; i++) {
            Item item = Inventory.Items[i];
            if (item.GetType() == itemType) {
                inventory.equippedItem = item;
                return;
            }
        }
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

    public void EquipStaff() {
        EquipItem(typeof(MagicalStaff));
    }
}
