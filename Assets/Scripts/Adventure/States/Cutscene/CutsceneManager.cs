using UnityEngine;

public class CutsceneManager : MonoBehaviour {
    AdventureManager Adventure => AdventureManager.Instance;
    AdventureManager.AdventureState previousState;

    bool checkForCutsceneEnding;

    private void OnEnable() {
        previousState = Adventure.State;
        checkForCutsceneEnding = true;
    }

    private void Update() {
        if (checkForCutsceneEnding) {
            if (StoryManager.Instance.Flowchart.GetExecutingBlocks().Count <= 0) {
                Adventure.ChangeState(previousState);
                checkForCutsceneEnding = false;
            }
        }
    }
}
