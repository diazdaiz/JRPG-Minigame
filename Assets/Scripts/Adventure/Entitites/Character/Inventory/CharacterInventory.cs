using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour {
    //int maxSlot = 20;
    public Item equippedItem;

    private void Awake() {
        Items = new();
        for (int i = 0; i < transform.childCount; i++) {
            Item item = transform.GetChild(i).GetComponent<Item>();
            if (item != null) {
                Items.Add(item);
            }
        }
    }

    public List<Item> Items { get; private set; }

    private void Update() {
        for (int i = 0; i < Items.Count; i++) {
            Item item = Items[i];
            if (equippedItem != null && equippedItem == item) {
                item.gameObject.SetActive(true);
            }
            else {
                //masih hardcoded, harusnya diatur di visual terpisah
                if (item is Axe axe && axe.swinging) {
                    item.gameObject.SetActive(true);
                }
                else if (item is Pickaxe pickaxe && pickaxe.swinging) {
                    item.gameObject.SetActive(true);
                }
                else {
                    item.gameObject.SetActive(false);
                }
            }
            if (item.transform.parent != this) {
                item.transform.parent = transform;
                item.transform.localPosition = new(-0.22f, 0.06f);
            }
        }
    }
}
