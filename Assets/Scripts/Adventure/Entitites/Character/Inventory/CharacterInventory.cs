using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour {
    //int maxSlot = 20;
    private void Awake() {
        Items = new();
    }

    public List<Item> Items { get; private set; }

    private void Update() {
        for (int i = 0; i < Items.Count; i++) {
            Item item = Items[i];
            if (item.transform.parent != this) {
                item.transform.parent = transform;
            }
        }
    }
}
