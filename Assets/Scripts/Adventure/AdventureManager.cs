using System;
using System.Collections.Generic;
using UnityEngine;

public class AdventureManager : MonoBehaviour {
    public static AdventureManager Instance { get; private set; } //bukan singleton, temporary aja biar mudah di akses
    public AdventureState State { get; private set; }
    public GameObject PortalssObjectsContainer => portalssObjectsContainer;

    [SerializeField] GameObject portalssObjectsContainer;
    [SerializeField] CombatManager combat;
    [SerializeField] List<StateManager> statesManager = new();


    [Serializable]
    public class StateManager {
        public AdventureState state;
        public GameObject gameObject;
    }
    public enum AdventureState { Roam, Cutscene, Combat }

    public void ChangeState(AdventureState state) {
        if (State == state) return;
        for (int i = 0; i < statesManager.Count; i++) {
            GameObject stateManagerGameobject = statesManager[i].gameObject;
            AdventureState stateManagerState = statesManager[i].state;
            if (stateManagerState == state) {
                stateManagerGameobject.SetActive(true);
            }
            else {
                stateManagerGameobject.SetActive(false);
            }
        }
        State = state;
    }

    public void InitiateCombat() {
        ChangeState(AdventureState.Combat);
        combat.Begin();
    }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            Debug.Log(Instance);
        }
    }

    private void Start() {
        State = AdventureState.Roam;
        ChangeState(AdventureState.Cutscene);
        //InitiateCombat();
    }
}
