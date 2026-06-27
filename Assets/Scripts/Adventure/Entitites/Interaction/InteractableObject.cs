using Fungus;
using UnityEngine;

public class InteractableObject : MonoBehaviour {
    public Interaction Interaction {
        get {
            return interaction;
        }
        set {
            interaction = value;
        }
    }
    public AdventureManager Adventure => AdventureManager.Instance;
    public Flowchart Flowchart => StoryManager.Instance.Flowchart;

    [SerializeField] Interaction interaction;
    [SerializeField] bool triggerOnEnter;

    public virtual Interaction Interact(GameObject interactor) {
        return Interaction;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (triggerOnEnter && collision.tag == "Interacting Area") {
            Debug.Log("Interacting Area masuk interacted area");
        }
    }
}
