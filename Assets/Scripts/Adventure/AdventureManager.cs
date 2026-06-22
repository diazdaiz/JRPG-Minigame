using System.Collections.Generic;
using UnityEngine;

public class AdventureManager : MonoBehaviour {
    public static AdventureManager Instance; //bukan singleton, temporary aja biar mudah di akses
    public ExplorationState State { get; private set; }
    public enum ExplorationState { Roam, Cutscene, Combat }

    Dictionary<ExplorationState, GameObject> statesGameobject = new();

    public void ChangeState(ExplorationState state) {
        if (State == state) return;
        State = state;
        foreach (ExplorationState _state in statesGameobject.Keys) {
            GameObject stateGameobject = statesGameobject[_state];
            if (_state == State) {
                stateGameobject.SetActive(true);
            }
            else {
                stateGameobject.SetActive(false);
            }
        }
    }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            Debug.Log(Instance);
        }
    }

    private void Start() {
        ChangeState(ExplorationState.Roam);
    }

    void Update() {

    }
}
