using UnityEngine;

public class Item : MonoBehaviour {
    public virtual void Use(InteractableObject interactableObject) {

    }

    public virtual void Equip() {
        gameObject.SetActive(true);
    }
}
