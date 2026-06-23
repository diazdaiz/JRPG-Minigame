using Fungus;
using UnityEngine;

public class TestDialogueInteraction : MonoBehaviour, Interactable {
    AdventureManager Adventure => AdventureManager.Instance;
    [SerializeField] Flowchart flowchart;

    public void Interact(GameObject interactor) {
        Debug.Log(Adventure);
        Adventure.ChangeState(AdventureManager.AdventureState.Cutscene);
        flowchart.ExecuteBlock("Introduction");
    }

    public void EndInteraction() {
        Adventure.ChangeState(AdventureManager.AdventureState.Roam);
    }
}
